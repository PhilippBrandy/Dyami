using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCamera : MonoBehaviour
{
    private Camera mainCamera;
    private float baseSize;
    private CameraBehaviour camControlls;

    private void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        camControlls = mainCamera.GetComponent<CameraBehaviour>();
        baseSize = mainCamera.orthographicSize;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            mainCamera.orthographicSize = 60;
            camControlls.offset.y = 30;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            mainCamera.orthographicSize = baseSize;
            camControlls.offset.y = 0;
        }
    }
}
