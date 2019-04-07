using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keystone : MonoBehaviour
{
    public RiddleManager owner;
    public GameObject activeEyes;
    public GameObject notactiveEyes;
    public bool activated;
    public string key;
    public Animator camera;
    // showing the effect to the player once
    public bool once;

    private void Start()
    {
        once = true;
        activated = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player") && activated == false)
        {
            if (other.GetComponentInChildren<ShootArrow>().theForce == true)
            {
                if (once)
                {
                    camera.applyRootMotion = false;
                    SetTimeDelayed(2f,camera);
                    camera.Play("showActivationOnce");
                    once = false;
                    camera.applyRootMotion = true;

                }
                activeEyes.SetActive(true);
                notactiveEyes.SetActive(false);
                activated = true;
                owner.collectedPassword += key;
            }
        }
    }
    IEnumerator SetTimeDelayed(float delayTime, Animator camera)
    {
        Debug.Log("animation triggered");
        camera.Play("showActivationOnce");
        yield return new WaitForSeconds(delayTime);  
    }
    public void deactivate()
    {
        activeEyes.SetActive(false);
        notactiveEyes.SetActive(true);
        activated = false;
    }
    
}
