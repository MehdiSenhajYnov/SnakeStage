using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakePart : MonoBehaviour
{
    private SnakeManager _smanager;
    public SnakeManager snakeManager { 
        get { 
            if (_smanager == null)
            {
                _smanager = GetComponentInParent<SnakeManager>();
            }
            return _smanager; 
        } 
    }
}
