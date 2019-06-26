using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enableMovement : MonoBehaviour
{
    public GameObject avatarController;
    public float maxSpeed=0;
    public float minSpeed = 0;
    public Animator animator;
    public string triggerName;

    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("camera trigger");
        if (collision.CompareTag("Player"))
        {
            animator.SetTrigger(triggerName);

            avatarController.GetComponent<PlayerMovement>().maxSpeed = maxSpeed;
            avatarController.GetComponent<PlayerMovement>().minSpeed = minSpeed;

      
        }
    }
}
