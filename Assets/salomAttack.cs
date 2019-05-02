using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class salomAttack : MonoBehaviour
{
    public Animator salmon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collider triggered");

        if (other.CompareTag("Player"))
        {
            Debug.Log("player enters");

                salmon.SetTrigger("Start");

        }
       
    }
}
