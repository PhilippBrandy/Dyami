using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eyeTrigger : MonoBehaviour
{
    public GameObject eyehandler;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("eye trigger set active(false)");
        eyehandler.SetActive(false);
    }
    

         void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collider collision");
        if (collision.CompareTag("Player"))
        {
            Debug.Log("collider player");

            eyehandler.SetActive(true);
        }
    }
}
