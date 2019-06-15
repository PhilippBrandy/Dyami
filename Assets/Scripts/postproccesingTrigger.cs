using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class postproccesingTrigger : MonoBehaviour
{
    public Animator animator;
    public string triggerName;


    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("camera trigger");
        if (collision.CompareTag("Player"))
        {

            animator.SetTrigger(triggerName);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {

    }
}
