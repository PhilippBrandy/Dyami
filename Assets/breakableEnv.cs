using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakableEnv : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.SetActive(false);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
