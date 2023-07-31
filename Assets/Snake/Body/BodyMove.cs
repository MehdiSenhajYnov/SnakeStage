using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyMove : MonoBehaviour, IMove
{
    BodyScale bodyScale;
    public BodyManager bodyManager;

    public BodyScale GetBodyScale()
    {
        if (bodyScale == null)
        {
            TryGetComponent(out bodyScale);

        }
        return bodyScale;
    }

    public IMove FollowedPart { get; set; }
    public void SetFollowedPart(IMove newFollowedPart)
    {
        FollowedPart = newFollowedPart;
    }

    // Chauque objet, suit un autre objet qui à son tour on suit un autre. Le seul qui ne suis pas un objectif est la tete
    bool followingHead => FollowedPart.FollowedPart == null;


    /* 
     Element at 0 -> Distance when Direction is Up
                1 ->  Distance when Direction is Down  
                2 ->  Distance when Direction is Left  
                3 ->  Distance when Direction is Right  
    */
    [SerializeField] Vector3[] Offsets = new Vector3[4];

    public Vector3[] GetOffets => Offsets;

    public event Action<Direction, Direction> OnChangeDirection;
    public Direction direction;

    bool Initialized()
    {
        return bodyManager != null;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public void SetDirection(Direction newDir)
    {
        direction = newDir;

        if (newDir.isHorizontal())
        {
            if (TryGetComponent(out SpriteRenderer spriteRenderer))
            {
                spriteRenderer.flipX = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (FollowedPart == null) return;
        if (!followingHead) return;
        transform.position = FollowedPart.transform.position + Offsets[(int)direction];
    }
}
