using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeDash : MonoBehaviour
{
    public int health = 1;
    public GameObject breakingApartEffect;
    private Killable player;
    public AudioSource breakSound;
    public Animator rotatingHinge;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Avatar new").GetComponent<Killable>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("update triggered");
        if (health < 1)
        {
            gameObject.SetActive(false);
            Instantiate(breakingApartEffect, transform.position, Quaternion.identity);
        }
        if (player.health == 0)
        {
            health = 1;
            gameObject.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collider triggered");

        if (other.CompareTag("Player"))
        {
            Debug.Log("destruction triggered");
            breakSound.Play();
            if (other.GetComponentInChildren<ShootArrow>().theForce == true)
            {
                health = 0;
                //rotatingHinge.Play("dashTree");
                rotatingHinge.SetTrigger("Start");
            }
        }
        if (other.CompareTag("falling"))
        {
            health = 0;
        }
    }
}
