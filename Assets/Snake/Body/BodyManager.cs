using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyManager : MonoBehaviour
{
    public HeadMove headMove;
    // Le corps le plus recent c'est toujours celui qui viens d'etre ajouté, alors que le plus vieux (celui que la queue va suivre, et toujours le premier)
    [SerializeField] List<BodyScale> SnakeBodies = new List<BodyScale>();
    BodyScale LastBody;

    public BodyScale testNew;
    public BodyScale testOld;

    public List<BodyScale> GetBodyMoves()
    {
        return SnakeBodies;
    }

    void Start()
    {
        if (GetOlderBody() != null)
        {
            Debug.Log("DECREASING!");
            GetOlderBody().bodyState = BodyState.Decreasing;
        }
        headMove.OnChangeDirection += DirectionChanged;
    }

    void DirectionChanged(Direction oldDirection,Direction newDirection)
    {
        BodyScale currentnewerbody = GetNewerBody();
        BodyScale currentolderbody = GetOlderBody();
        // Si pour quelque raison le corps est null ne fait rien
        if (currentolderbody == null || currentnewerbody == null) return;

        if (SnakeBodies.Count == 1)
        {
            // quand il y a qu'un seul corp et qu'on tourne, le corps de base commence à se reduire
            currentolderbody.bodyState = BodyState.Decreasing;
        } else
        {
            // Le corps au milieu doit etre en mode d'attente
            currentnewerbody.bodyState = BodyState.Idle;
        }


    }

    void Update()
    {
        testNew = GetNewerBody();
        testOld = GetOlderBody();
    }


    public void AddBody(BodyScale newbody)
    {
        SnakeBodies.Add(newbody);
        newbody.bodyState = BodyState.Growing;
        newbody.transform.name = $"BODY {SnakeBodies.Count}";

        CheckLastBody();
    }

    void CheckLastBody()
    {
        if (SnakeBodies.Count == 0) return;
        for (int i = 0; i < SnakeBodies.Count; i++)
        {
            if (SnakeBodies[i] == null) continue;
            if (SnakeBodies[i].transform == null) continue;

            SnakeBodies[i].isLast = false;
        }
        GetOlderBody().isLast = true;
    }

    public void RemoveBody(BodyScale bodyToRemove)
    {
        SnakeBodies.Remove(bodyToRemove);

        BodyScale currentnewerbody = GetNewerBody();
        BodyScale currentolderbody = GetOlderBody();

        if (currentnewerbody != currentolderbody)
        {
            //currentolderbody.bodyState = BodyState.Decreasing;
        }
        else
        {
            // il y a qu'un seul corp
           //currentolderbody.bodyState = BodyState.Idle;
        }

        CheckLastBody();

    }

    public BodyScale GetOlderBody()
    {
        CheckIfBodyIsValid(0);
        if (SnakeBodies.Count > 0)
        {
            return SnakeBodies[0];
        }
        return null;
    }
    public BodyScale GetNewerBody()
    {
        CheckIfBodyIsValid(SnakeBodies.Count - 1);
        if (SnakeBodies.Count > 0)
        {
            return SnakeBodies[SnakeBodies.Count - 1];
        }
        return null;
    }

    public void CheckIfBodyIsValid(int atIndex)
    {
        if (atIndex < 0) return;
        if (atIndex >= SnakeBodies.Count) return;
        while (SnakeBodies.Count > 0 && SnakeBodies[atIndex] == null)
        {
            SnakeBodies.RemoveAt(atIndex);
        }
    }


}
