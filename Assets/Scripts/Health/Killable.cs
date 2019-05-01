using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Killable : MonoBehaviour
{
    public int health;
    public int numOfLives;

    public Image[] lives;
    public Sprite fullLive;
    public Sprite emptyLive;

    public Transform spawnpoint;
    private int count = 10;

    void Start()
    {
        health = numOfLives;
    }

    void Update()
    {

        if (health > numOfLives)
        {
            health = numOfLives;
        }

        for (int i = 0; i < lives.Length; i++)
        {
            if (i < health)
            {
                lives[i].sprite = fullLive;
            }
            else
            {
                lives[i].sprite = emptyLive;
            }

            if (i < numOfLives)
            {
                lives[i].enabled = true;
            }
            else
            {
                lives[i].enabled = false;
            }
        }


        if (health < 0)
        {
            if (spawnpoint == null)
            {
                Application.LoadLevel(Application.loadedLevel);
            }
            transform.position = spawnpoint.position;
            health = numOfLives;
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
