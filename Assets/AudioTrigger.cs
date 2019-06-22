using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public GameObject audioObj;
    public bool activate;
    public bool deactivation;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("audio trigger");
        if (collision.CompareTag("Player") && activate)
        {
            audioObj.SetActive(true);
        }

        if (collision.CompareTag("Player") && deactivation)
        {
            audioObj.SetActive(false);
        }
    }
}