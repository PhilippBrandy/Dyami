using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateTotemText : MonoBehaviour
{
    public GameObject textObject;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("enter");
  textObject.SetActive(true);
        Invoke("removeText", 20);
          
            }
          
    }




    private void removeText()
    {
        textObject.SetActive(false);
    }

}