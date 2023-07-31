using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HeadMove))]
public class HeadControls : MonoBehaviour
{
    HeadMove headMove;
    // Start is called before the first frame update
    void Start()
    {
        headMove = GetComponent<HeadMove>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            headMove.setDirection(Direction.Up);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            headMove.setDirection(Direction.Left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            headMove.setDirection(Direction.Right);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            headMove.setDirection(Direction.Down);
        }
    }

    void DirectionInput(Direction dir)
    {
        headMove.setDirection(dir);
    }


}
