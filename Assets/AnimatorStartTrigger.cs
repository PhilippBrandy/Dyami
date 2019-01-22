using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorStartTrigger : MonoBehaviour
{
    public Animator animator;
    

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetTrigger("Start");
        }
    }
}
