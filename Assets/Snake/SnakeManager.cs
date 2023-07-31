using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeManager : MonoBehaviour
{
    [Header("Components")]
    public HeadMove head;
    public BodyManager bodyManager;
    public float speed = 2;
    public float TargetSize = 5;
    public List<GameObject> Connectors;

    public float GetCurrentSize()
    {
        float curSize = 0;
        foreach(BodyScale body in bodyManager.GetBodyMoves())
        {
            curSize += body.BodySize;
        }
        curSize += Connectors.Count;
        return curSize;
    }

    public float testCurrentSize;
    private void Update()
    {
        testCurrentSize = GetCurrentSize();
    }

    private void Start()
    {
        //Application.targetFrameRate = 20;
        //Debug.Log("FPS LOCK TO 20 TO TEST ON LOW END DEVICE");
    }
}
