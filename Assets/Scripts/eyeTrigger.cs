using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eyeTrigger : MonoBehaviour
{
    public Animator openEye;
    public GameObject eyeGlow;
    public GameObject eyehandler;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("eye trigger set active(false)");
        eyehandler.SetActive(false);
        eyeGlow.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Animation should start");

            openEye.SetTrigger("Start");
            eyehandler.SetActive(true);
            eyeGlow.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Animation should end");

       
            eyehandler.SetActive(false);
            eyeGlow.SetActive(false);
        }
    }
}
