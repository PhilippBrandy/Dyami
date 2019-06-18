using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalldownPlatform : MonoBehaviour {
    public GameObject cuttable;
    public float mass = 5;
    private bool isHanging = true;
    public float gravity;
    private Rigidbody2D myRigidbody = null;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (cuttable == null && myRigidbody == null)
        {
            myRigidbody = gameObject.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D; // Add the rigidbody.
            myRigidbody.mass = mass;
            myRigidbody.gravityScale = gravity;
            isHanging = false;
            gameObject.tag = "falling";
        }
        
	}
}
