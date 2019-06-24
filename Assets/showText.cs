using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showText : MonoBehaviour
{
    public GameObject obj;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("text trigger");
        if (collision.CompareTag("Player"))
        {
            obj.SetActive(true);

        }
        
    }
    void OnTriggerExit2D(Collider2D collision)
        {
        if (collision.CompareTag("Player"))
        {
            obj.SetActive(false);


        }

    }
}