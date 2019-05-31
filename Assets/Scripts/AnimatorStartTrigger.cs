using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorStartTrigger : MonoBehaviour
{
    public Animator animator;
    public string triggerName;
    public Camera camera;
    

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("camera trigger");
        if (collision.CompareTag("Player"))
        {
            camera.GetComponent<CameraBehaviour>().enabled = false;
            
            animator.SetTrigger(triggerName);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {

        camera.GetComponent<CameraBehaviour>().enabled = true;
    }
}
