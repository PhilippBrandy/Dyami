using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalldownPlatform : MonoBehaviour {
    public GameObject cuttable;
    public float mass = 5;
    private bool isHanging = true;
    public float gravity;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (cuttable == null)
        {
            Rigidbody2D myRigidbody = gameObject.AddComponent<Rigidbody2D>(); // Add the rigidbody.
            myRigidbody.mass = mass;
            myRigidbody.gravityScale = gravity;
            isHanging = false;
            gameObject.tag = "falling";
        }
        
	}
}
