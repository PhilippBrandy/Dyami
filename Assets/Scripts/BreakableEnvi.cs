﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableEnvi : MonoBehaviour {

    public int health = 1;
    public GameObject breakingApartEffect;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (health < 1)
        {
            Destroy(gameObject);
            Instantiate(breakingApartEffect, transform.position, Quaternion.identity);
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("destruction triggered");
            if (other.GetComponentInChildren<ShootArrow>().theForce == true)
            {
                health = 0;
            }
        }
        if (other.CompareTag("falling"))
        {
            health = 0;
        }
    }
}