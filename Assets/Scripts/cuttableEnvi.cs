using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cuttableEnvi : MonoBehaviour {

    
    public int health = 1;

	
	// Update is called once per frame
	void Update () {
		if (health < 1)
        {
            Destroy(gameObject);
        }
	}

}
