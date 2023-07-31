using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PartCreator : SnakePart
{
    [Header("Body")]
    [SerializeField] GameObject BodyPrefab;
    [SerializeField] Transform BodyParent;
    [SerializeField] BodyManager bodyManager;
    public BodyManager GetBodyManager => bodyManager;

    IMove currentBody;

    [Header("Connector")]
    [SerializeField] GameObject ConnectionPrefab;
    [SerializeField] GameObject ConnectionPrefabAlt;
    [SerializeField] Transform ConnectionParent;

    [Header("Tail")]
    [SerializeField] TailMove tailPrefab;
    [SerializeField] TailMove tail;
    [SerializeField] Transform TailParent;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitSnake()
    {

    }

    public void CreateBody(IMove FollowedPart, Direction newDir, float beginSize = -1)
    {
        var bodyObj = Instantiate(BodyPrefab, BodyParent);
        BodyMove bodyMove = bodyObj.GetComponent<BodyMove>();
        bodyMove.SetFollowedPart(FollowedPart);
        bodyMove.SetDirection(newDir);

        currentBody = bodyMove;
        currentBody.transform.RotateTo(newDir);

        if (snakeManager.Connectors.Count > 0)
        {
            bodyMove.GetBodyScale().ConnectorToWait = snakeManager.Connectors[snakeManager.Connectors.Count - 1];
            bodyObj.transform.position = snakeManager.Connectors[snakeManager.Connectors.Count - 1].transform.GetChild(0).GetChild(0).position;
        }
        else
        {
            bodyObj.transform.position = bodyMove.FollowedPart.transform.position + bodyMove.GetOffets[(int)newDir];
        }

        if (beginSize != -1)
        {
            var beginBodySize = bodyObj.transform.localScale;
            beginBodySize.y = beginSize;
            bodyObj.transform.localScale = beginBodySize;
        }

        bodyObj.SetActive(true);
    }

    public void CreateConnection(Direction oldDirection, Direction newDirection)
    {
        string dirName = oldDirection.ToString() + "-" + newDirection.ToString();

        BodyMove bodyMove = (BodyMove)currentBody;
        if (bodyMove != null)
            bodyMove.enabled = false;

        GameObject connector;
        if (dirName == "Up-Left" || dirName == "Down-Right" || dirName == "Right-Down" || dirName == "Left-Up")
        {
            connector = Instantiate(ConnectionPrefabAlt, ConnectionParent);
        } else
        {
            connector = Instantiate(ConnectionPrefab, ConnectionParent);
        }

        connector.transform.position = transform.position;

        // Connection Rotation
        Vector3 rot = ConnectionPrefab.transform.rotation.eulerAngles;
        connector.transform.rotation = Quaternion.Euler(rot);
        if (oldDirection.isRight(newDirection))
        {
            Vector3 currentScale = connector.transform.localScale;
            currentScale.x *= -1;
            connector.transform.localScale = currentScale;
            foreach (Transform child in connector.transform)
            {
                child.tag = "ConnectorRight";
            }
        } else
        {
            foreach (Transform child in connector.transform)
            {
                child.tag = "ConnectorLeft";
            }
        }


        connector.SetActive(true);
        snakeManager.Connectors.Add(connector);
        connector.GetComponent<TwoStepVirage>().CreateOneThenTwo(0);
    }

    public void CreateTail()
    {
        tail = Instantiate(tailPrefab, TailParent);
        tail.bodyManager = bodyManager;
    }

}
