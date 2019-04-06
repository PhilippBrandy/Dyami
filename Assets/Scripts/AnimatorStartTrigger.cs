using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorStartTrigger : MonoBehaviour
{
    public Animator animator;
    

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("camera trigger");
        if (collision.CompareTag("Player"))
        {
            animator.SetTrigger("Start");
        }
    }
}
