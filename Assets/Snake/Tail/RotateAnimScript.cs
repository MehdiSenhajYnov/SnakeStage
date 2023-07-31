using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAnimScript : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // TEST

        /*
        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.Play("RotateLeft");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            animator.Play("RotateRight");
        }
        */
    }

    public void Idle()
    {
        animator.Play("Idle");
    }

    public void RotateLeft()
    {
        animator.Play("RotateLeft");
    }

    public void RotateRight()
    {
        animator.Play("RotateRight");
    }

}
