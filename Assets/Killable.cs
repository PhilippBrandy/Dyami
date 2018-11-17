using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour {

    public int health;
    public Transform spawnpoint;
    private int count = 10;

	// Use this for initialization
	void Start () {
        health = 1;
        
	}
	
	// Update is called once per frame
	void Update () {
        if (health < 1)
        {
            if (spawnpoint == null)
            {
                Application.LoadLevel(Application.loadedLevel);
            }
            transform.position = spawnpoint.position;
            health = 1;
        }
	}

    // DamageTrigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("damaging"))
        {
            health--;
        }
    }

}
