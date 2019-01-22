using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessingTrigger : MonoBehaviour
{
    public Animator vignette;
    
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
        if (collision.CompareTag("Player"))
        {
            vignette.SetTrigger("Start");


        }
    }
}
