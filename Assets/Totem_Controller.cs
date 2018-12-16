using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem_Controller : MonoBehaviour {
    public GameObject passiveTotem;
    public GameObject activeTotem;
    public GameObject lightningFX;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            passiveTotem.SetActive(false);
            activeTotem.SetActive(true);
            Instantiate(lightningFX, transform.position, Quaternion.identity);
        }
    }
}
