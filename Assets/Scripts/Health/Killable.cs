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

    // Invulnerability after getting damaged
    bool invincible = false;
    public float secondsInvulnerable = 3f;
    float timer = 0.0f;
    bool timerIsActive = false;

    // push player away from damaging source
    Rigidbody2D rb;
    public float knockDur = 0.01f;

    void Start()
    {
        health = numOfLives;
        rb = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {

        if (health > numOfLives)
        {
            health = numOfLives;
        }

        if (timerIsActive)
        {
            timer += Time.deltaTime;
            if (timer >= secondsInvulnerable)
            {
                timerIsActive = false;
                invincible = false;
                timer = 0.0f;
            }
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
            //if (spawnpoint == null)
            //{
            //    Application.LoadLevel(Application.loadedLevel);
            //}
            //transform.position = spawnpoint.position;
            //health = numOfLives;
        }
    }

    // DamageTrigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("damaging"))
        {
            if (!invincible && !timerIsActive)
            {
                health--;

                // for invulnerability
                invincible = true;
                timerIsActive = true;
                
               // StartCoroutine(Knockback(240, transform.position));
            }
        }
    }

    public IEnumerator Knockback(float knockbackPwr, Vector3 knockbackDir)
    {
        float timer = 0;
        while (knockDur > timer)
        {
            timer += Time.deltaTime;
            //rb.AddForce(new Vector3(knockbackDir.x * -100, knockbackDir.y * knockbackPwr * -1, transform.position.z));
            //rb.AddForce(new Vector3(knockbackDir.x * -100, knockbackDir.y * knockbackPwr * -100, transform.position.z));
        }
        yield return 0;
    }
}
