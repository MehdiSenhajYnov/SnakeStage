using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectorFollowable : MonoBehaviour, IFollowable
{
    [field: SerializeField] public List<Transform> FollowPoint { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
