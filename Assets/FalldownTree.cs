using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalldownTree : MonoBehaviour {

    public ParticleSystem BreakApartFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Instantiate(BreakApartFX, gameObject.transform);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
