using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setActiveObj : MonoBehaviour
{
    public GameObject[] ObjectsetActive;
    public GameObject[] ObjectsetPassive;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") )
        {
            for(int i=0; i< ObjectsetActive.Length; i++)
            {
            ObjectsetPassive[i].SetActive(false);

            }
            for (int i = 0; i < ObjectsetActive.Length; i++)
            {
                ObjectsetActive[i].SetActive(true);

            }
        }

    }
}