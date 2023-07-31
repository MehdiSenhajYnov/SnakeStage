using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PartCreator))]
public class HeadMove : SnakePart, IMove
{
    PartCreator partCreator;
    public BodyManager GetBodyManager()
    {
        if (partCreator == null) return null;
        return partCreator.GetBodyManager;
    }
    [SerializeField] Direction direction;
    [SerializeField] float speed = 2;
    [SerializeField] Vector3 directionMove;
    [SerializeField] bool Rotating;

    public event Action<Direction,Direction> OnChangeDirection;

    public Direction getDirection => direction;

    public IMove FollowedPart {get; set;}

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        partCreator = GetComponent<PartCreator>();
        direction = Direction.Up;


        partCreator.CreateBody(this,direction,snakeManager.TargetSize);
        partCreator.CreateTail();


    }

#if UNITY_EDITOR
    static HeadMove hm;
    public static HeadMove GetHm()
    {
        if (hm == null)
        {
            hm = (HeadMove)FindObjectOfType(typeof(HeadMove));
        }
        return hm;
    }

    [UnityEditor.MenuItem("SimulateKey/GoUp")]
    public static void GoUp()
    {
        GetHm().setDirection(Direction.Up);
    }

    [UnityEditor.MenuItem("SimulateKey/GoDown")]
    public static void GoDown()
    {
        GetHm().setDirection(Direction.Down);
    }

    [UnityEditor.MenuItem("SimulateKey/GoLeft")]
    public static void GoLeft()
    {
        GetHm().setDirection(Direction.Left);
    }

    [UnityEditor.MenuItem("SimulateKey/GoRight")]
    public static void GoRight()
    {
        GetHm().setDirection(Direction.Right);
    }
#endif

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        if (direction == Direction.Up)
        {
            ChangeDirectionMove(Vector3.up);
        }
        else if (direction == Direction.Left)
        {
            ChangeDirectionMove(Vector3.left);
        }
        else if (direction == Direction.Right)
        {
            ChangeDirectionMove(Vector3.right);
        }
        else if (direction == Direction.Down)
        {
            ChangeDirectionMove(Vector3.down);
        }
        if (Rotating) return;
        PerformMovement(directionMove);
        //transform.RotateToSmooth(direction);
    }

    public void PerformMovement(Vector3 dir)
    {
        transform.position += dir * speed * Time.deltaTime;
    }
    public void setDirection(Direction newDirection)
    {
        if (Rotating) return;
        if (getDirection.isOpposite(newDirection)) return;
        if (direction == newDirection) return;
        Rotating = true;
        partCreator.CreateConnection(direction, newDirection);

        if (direction.isLeft(newDirection))
        {
            RotateLeft();
        } else if (direction.isRight(newDirection))
        {
            RotateRight();
        }



        OnChangeDirection?.Invoke(direction,newDirection);
        direction = newDirection;

    }

    public void ChangeDirectionMove(Vector3 newdirectionMove)
    {
        directionMove = newdirectionMove;
    }

    // Called by animation event
    public void StopRotating()
    {
        Rotating = false;
        partCreator.CreateBody(this, direction);

    }

/*
    public void Idle()
    {
        animator.Play("Idle");
    }
*/

    public void RotateLeft()
    {
        animator.Play("RotateLeft");
    }

    public void RotateRight()
    {
        animator.Play("RotateRight");
    }

}

public enum Direction
{
    Up,
    Down,
    Left,
    Right,
}