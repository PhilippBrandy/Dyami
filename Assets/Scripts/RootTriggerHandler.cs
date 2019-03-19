using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootTriggerHandler : MonoBehaviour
{
    public Animator animator;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetTrigger("Start");
            animator.ResetTrigger("Stop");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        animator.SetTrigger("Stop");
        animator.ResetTrigger("Start");
    }
}
