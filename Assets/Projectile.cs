using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public GameObject destroyEffect;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("cuttable"))
        {
            other.GetComponent<cuttableEnvi>().health = 0;
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
        }
    }
}
