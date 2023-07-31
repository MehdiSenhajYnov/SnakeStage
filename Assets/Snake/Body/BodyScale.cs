using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyScale : SnakePart
{
    public BodyManager bodyManager;
    public BodyState bodyState = BodyState.Idle;
    [SerializeField] float GrowSpeed;

    public bool isLast;
    public float BodySize;
    public GameObject ConnectorToWait;


    // Start is called before the first frame update
    void Start()
    {
        bodyManager.AddBody(this);
    }
    Vector3 currentScale;
    // Update is called once per frame
    void Update()
    {
        if (bodyState == BodyState.Decreasing && transform.localScale.y < 0.1f)
        {
            bodyManager.RemoveBody(this);
            Destroy(gameObject);
            this.enabled = false;
            return;
        }
        if (bodyState == BodyState.Growing)
        {
            currentScale = transform.localScale;
            currentScale.y += GrowSpeed * Time.deltaTime;
            transform.localScale = currentScale;
            BodySize = currentScale.y;
            if (currentScale.y >= snakeManager.TargetSize)
            {
                // si on arrive à la taille désideré on arrete de grandir!
                bodyState = BodyState.Idle;

            }
        }
        else if (bodyState == BodyState.Decreasing)
        {
            currentScale = transform.localScale;
            currentScale.y -= GrowSpeed * Time.deltaTime;
            if (currentScale.y <= 0) currentScale.y = 0;
            transform.localScale = currentScale;
            BodySize = currentScale.y;

        }


        if (isLast && ConnectorToWait == null)
        {

            if (snakeManager.GetCurrentSize() < snakeManager.TargetSize)
            {
                bodyState = BodyState.Growing;
            } else if (this == bodyManager.GetOlderBody())
            {
                bodyState = BodyState.Decreasing;
            } else
            {
                bodyState = BodyState.Idle;
            }

        }

        // Test
        if (Input.GetKeyDown(KeyCode.Space))
        {
            snakeManager.TargetSize += 1;
        }
    }
}

public enum BodyState
{
    Growing,
    Decreasing,
    Idle,
}