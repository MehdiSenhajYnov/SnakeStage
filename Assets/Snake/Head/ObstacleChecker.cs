using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleChecker : MonoBehaviour
{
    GameObject Head;
    // Start is called before the first frame update
    void Start()
    {
        Head = transform.parent.gameObject;
    }

    Collider2D[] colliders;

    // Update is called once per frame
    void Update()
    {
        colliders = Physics2D.OverlapPointAll(transform.position);
        if(colliders.Length >= 1)
        {
            foreach (var item in colliders)
            {
                if (item.gameObject != Head)
                {
                    Debug.Log("Game Over");
                }
            }
        }   

    }
}
