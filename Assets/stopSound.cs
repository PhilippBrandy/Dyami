using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopSound : MonoBehaviour
{
    public AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            sound.Stop();

        }
    }
}
