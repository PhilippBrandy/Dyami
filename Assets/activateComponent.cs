using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateComponent : MonoBehaviour
{
    //public BuoyancyEffector2D comp;
    public Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        // comp = GetComponent<BuoyancyEffector2D>();
        //   comp.enabled = !comp.enabled;
        rigidbody.simulated = false;
        Debug.Log("simulated: " + rigidbody.simulated);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            rigidbody.simulated = true;

            //Debug.Log("Animation should start");
            /// comp = GetComponent<BuoyancyEffector2D> ();
            // comp.enabled = comp.enabled;
        }
    }
}
