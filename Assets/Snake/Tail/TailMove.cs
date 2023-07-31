using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TailRotate))]
public class TailMove : MonoBehaviour
{
    public BodyManager bodyManager;
    TailRotate tailRotate;

    public GameObject FollowedPart { get; set; }
    public BodyMove FBodyMove;

    Transform BodyEnd;
    Transform GetBodyEnd()
    {
        if (BodyEnd != null) return BodyEnd;
        if (FollowedPart == null) return null;
        BodyEnd = FollowedPart.transform.GetChild(0);
        return BodyEnd;
    }

    bool followedBodyChanged;
    public void SetFollowedPart(GameObject newFollowedPart)
    {
        FollowedPart = newFollowedPart;
        newFollowedPart.TryGetComponent(out FBodyMove);
        direction = FBodyMove.direction;
    }

    public event Action<Direction> OnChangeDirection;
    public Direction direction;

    bool Initialized()
    {
        return FollowedPart != null;
    }
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitUntil(Initialized);
        tailRotate = GetComponent<TailRotate>();
    }


    // Update is called once per frame
    void Update()
    {
        if (!isFollowedPartValid())
        {
            return;
        }

        if (tailRotate != null && tailRotate.Rotating) return;
        transform.position = GetBodyEnd().position;
        transform.rotation = GetBodyEnd().rotation;

        if (GetCurrentDirection() == Direction.Up)
        {
            transform.position += new Vector3(0, 0.08f, 0);
        }
        else if (GetCurrentDirection() == Direction.Down)
        {
            transform.position += new Vector3(0, -0.08f, 0);
        }
        else if (GetCurrentDirection() == Direction.Left)
        {
            transform.position += new Vector3(-0.08f, 0, 0);
        }
        else if (GetCurrentDirection() == Direction.Right)
        {
            transform.position += new Vector3(0.08f, 0, 0);
        }

    }

    bool isFollowedPartValid()
    {
        
        if (FollowedPart != null) return true;
        if (bodyManager == null) return false;

        var older = bodyManager.GetOlderBody();
        if (older == null) return false;
        
        SetFollowedPart(older.gameObject);
        return true;
    }

    public Direction GetCurrentDirection()
    {
        float rotation = transform.rotation.eulerAngles.z;

        // Normalisation de la rotation entre 0 et 360 degrés
        while (rotation < 0f)
        {
            rotation += 360f;
        }

        if (rotation >= 360f)
        {
            rotation %= 360f;
        }

        if (rotation > 315f || rotation <= 45f)
        {
            return Direction.Up;
        }
        else if (rotation > 45f && rotation <= 135f)
        {
            return Direction.Left;
        }
        else if (rotation > 135f && rotation <= 225f)
        {
            return Direction.Down;
        }
        else if (rotation > 225f && rotation <= 315f)
        {
            return Direction.Right;
        }

        // Si aucun cas ne correspond, vous pouvez renvoyer une direction par défaut ici
        return Direction.Up;
    }
}
