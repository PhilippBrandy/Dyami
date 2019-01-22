using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagletrigger : MonoBehaviour
{
    public Animator eagle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            eagle.SetTrigger("Start");
            

        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
