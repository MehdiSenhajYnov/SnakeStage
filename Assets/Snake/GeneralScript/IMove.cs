using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMove
{
    Transform transform { get; }
    public bool enabled { get; set; }
    public event Action<Direction, Direction> OnChangeDirection;
    IMove FollowedPart { get; set; }

}
