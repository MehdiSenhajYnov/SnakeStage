using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TailRotate : SnakePart
{

    Animator animator;

    [Header("Detector")]
    Transform ConnectorDetector;
    Collider2D[] colliders;
    Transform CurrentFollowedTransform;
    IFollowable currentFollowed;

    [Header("RotationWaypoints")]
    public bool Rotating = true; // Booléen indiquant si l'objet suit les checkpoints
    public Transform[] TurnWaypoints;
    public float rotationSpeed = 5f;


    //RotateAnimScript rotateAnimScript;

    private void Start()
    {
        if (ConnectorDetector == null)
        {
            ConnectorDetector = transform.GetChild(0);
        }
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Rotating) return;

        DestroyOldConnector();

        ConnectorChecking();

    }

    void DestroyOldConnector()
    {
        if (CurrentFollowedTransform != null && CurrentFollowedTransform.gameObject != null)
        {
            snakeManager.Connectors.Remove(CurrentFollowedTransform.gameObject);
            Destroy(CurrentFollowedTransform.gameObject);
        }
    }

    void ConnectorChecking()
    {
        colliders = Physics2D.OverlapPointAll(ConnectorDetector.position);
        if (colliders != null && colliders.Length > 0)
        {
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.CompareTag("ConnectorRight"))
                {
                    if (collider.transform.parent != CurrentFollowedTransform && collider.TryGetComponent(out currentFollowed))
                    {
                        CurrentFollowedTransform = collider.transform.parent;
                        Debug.Log("CONNECTOR FOUND");

                        //turnDirection = TurnDirection.Right;
                        StartRotating(TurnDirection.Right);
                        if (CurrentFollowedTransform.TryGetComponent(out TwoStepVirage twoStepVirage))
                        {
                            twoStepVirage.DestroyOneThenTwo(0);
                        }
                    }
                }
                if (collider.gameObject.CompareTag("ConnectorLeft"))
                {
                    if (collider.transform.parent != CurrentFollowedTransform && collider.TryGetComponent(out currentFollowed))
                    {
                        CurrentFollowedTransform = collider.transform.parent;

                        //turnDirection = TurnDirection.Left;

                        StartRotating(TurnDirection.Left);
                        if (CurrentFollowedTransform.TryGetComponent(out TwoStepVirage twoStepVirage))
                        {
                            twoStepVirage.DestroyOneThenTwo(0);
                        }
                    }
                }

            }
        }
    }

    public void StartRotating(TurnDirection turnDirection)
    {
        Rotating = true;
        if (turnDirection == TurnDirection.Right)
        {
            RotateRight();

        }
        else if (turnDirection == TurnDirection.Left)
        {
            RotateLeft();
        }
    }

    // Called by animation event
    public void StopRotating()
    {
        Rotating = false;
    }




    public void RotateLeft()
    {
        animator.Play("RotateLeft");
    }

    public void RotateRight()
    {
        animator.Play("RotateRight");
    }

    public enum TurnDirection
    {
        Left,
        Right
    }
 
    

}

