﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalldownPlatform : MonoBehaviour {

    private bool isHanging = true;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.childCount == 0 && isHanging)
        {
            Rigidbody2D myRigidbody = gameObject.AddComponent<Rigidbody2D>(); // Add the rigidbody.
            myRigidbody.mass = 5;
            isHanging = false;
            gameObject.tag = "falling";
        }
	}
}
