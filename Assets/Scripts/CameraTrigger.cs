using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public Animator camAnimator;
    public string triggerName;
    public Camera camera;
    public GameObject avatarController;
    public float waitTime;
    private IEnumerator coroutine;
    public bool triggerAnimation;


    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("camera trigger");
        if (collision.CompareTag("Player")&&triggerAnimation)
        {
            Debug.Log("camera trigger hello");

            camera.GetComponent<CameraBehaviour>().enabled = false;
            avatarController.GetComponent<PlayerMovement>().enabled = false;

            camAnimator.SetTrigger(triggerName);
            coroutine = WaitAndWatch(waitTime);
            StartCoroutine(coroutine);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        triggerAnimation = false;


    }

    private IEnumerator WaitAndWatch(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        camera.GetComponent<CameraBehaviour>().enabled = true;
        avatarController.GetComponent<PlayerMovement>().enabled = true;

    }
}
