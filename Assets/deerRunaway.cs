using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deerRunaway : MonoBehaviour
{ public Animator runAway;
    public string triggerName;
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
        Debug.Log("enter:" + triggerName);
        //nur wenn der spieler beim savepoint wiederbelebt wird soll triggeranimation mit reset akiviert werden
        if (collision.CompareTag("Player") )
        {
            Debug.Log("enter:" + collision.name);
            // animatorWater.StopPlayback();
            runAway.SetTrigger(triggerName);
        }
    }
}