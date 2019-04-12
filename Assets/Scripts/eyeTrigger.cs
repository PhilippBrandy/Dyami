using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eyeTrigger : MonoBehaviour
{
    public Animator openEye;
    public GameObject eyeGlow;
    public GameObject eyehandler;
    public GameObject stoneEye;

    void Start()
    {
        eyeGlow.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Animation should start");
            openEye.SetTrigger("Start");
            eyehandler.transform.parent = collision.transform;
            eyeGlow.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Animation should end");
            eyehandler.transform.parent = stoneEye.transform;
            eyeGlow.SetActive(false);
        }
    }
}
