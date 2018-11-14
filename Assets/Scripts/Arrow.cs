using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    public float speed = 2f;
    public Rigidbody2D rb;

    private void Start()
    {
 
        rb.AddForce(new Vector2(10, 10));
    }

    void FixedUpdate () {


    }
}
