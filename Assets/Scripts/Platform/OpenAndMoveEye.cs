using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAndMoveEye : MonoBehaviour
{
    public Animator openEye;
    public GameObject eyeGlow;
    public GameObject eyehandler;
    public GameObject stoneEye;
    public Transform eyeTarget;
    Vector3 origPosition;

    void Start()
    {
        eyeGlow.SetActive(false);
        origPosition = transform.position;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Debug.Log("Animation should start");
            openEye.SetTrigger("Start");
            eyehandler.transform.parent = collision.transform;
            eyeGlow.SetActive(true);
            eyehandler.transform.position = eyeTarget.transform.position;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Debug.Log("Animation should end");
            eyehandler.transform.parent = stoneEye.transform;
            eyeGlow.SetActive(false);
            eyehandler.transform.position = origPosition;
        }
    }
}
