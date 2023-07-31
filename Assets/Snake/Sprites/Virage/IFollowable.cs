using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFollowable
{
    public List<Transform> FollowPoint { get; set; }
}
