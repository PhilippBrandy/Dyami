using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalldownTree : MonoBehaviour {

    private Rigidbody2D[] childrenPhysix;

    public ParticleSystem BreakApartFX;

    private void Awake()
    {
        childrenPhysix = GetComponentsInChildren<Rigidbody2D>();
        foreach (Rigidbody2D rigidBody in childrenPhysix)
            {
            rigidBody.bodyType = RigidbodyType2D.Static;
            }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Instantiate(BreakApartFX, gameObject.transform);
            foreach (Rigidbody2D rigidBody in childrenPhysix)
            {
                rigidBody.bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }
}
