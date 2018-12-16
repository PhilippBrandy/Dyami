using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalldownTree : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D myRigidbody = gameObject.AddComponent<Rigidbody2D>(); // Add the rigidbody.
        myRigidbody.mass = 5;
        gameObject.tag = "falling";
    }
}
