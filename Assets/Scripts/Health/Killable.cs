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
    public AudioClip[] characterSoundFx;
    public Transform spawnpoint;
    private int count = 10;
    public AudioSource audioSource;
    public AudioSource grunt;
    //Shows if player is in the death anymation or not (true = not in death animation)
    private bool respawned = true;

    // Invulnerability after getting damaged
    bool invincible = false;
    public float secondsInvulnerable = 2f;
    float timer = 0.0f;
    bool timerIsActive = false;

    // push player away from damaging source
    Rigidbody2D rb;
    public int horizontalForce = 7000;
    public int verticalForce = 4000;
    // player can*t move during knockback
    float knockBackTimer = 0.0f;
    public float secondsKnockout = 2f;
    bool knockoutTimerIsActive = false;
    public GameObject DeathAnim;


    void Start()
    {
        health = numOfLives;
        rb = GetComponent<Rigidbody2D>();

    }

    IEnumerator SetTimeDelayed(float delayTime)
    {
        Debug.Log("animation triggered");

        yield return new WaitForSeconds(delayTime);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            health--;
        }

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


        if (knockoutTimerIsActive)
        {
            timer += Time.deltaTime;
            this.GetComponent<PlayerMovement>().DisablePlayerMovement();
            if (timer >= secondsKnockout)
            {
                knockoutTimerIsActive = false;
                invincible = false;
                timer = 0.0f;
                this.GetComponent<PlayerMovement>().EnablePlayerMovement();
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

        if (health < 0 && respawned)
        {
            respawned = false;
            GetComponentInParent<PlayerMovement>().enabled = false;
            GetComponentInParent<ShootArrow>().enabled = false;
            DeathAnim.SetActive(true);

            SpriteRenderer[] renderers;
            renderers = GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer renderer in renderers)
            {
                renderer.enabled = false;
            }
            DeathAnim.GetComponent<SpriteRenderer>().enabled = true;

            Invoke("respawn", 1);
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
                audioSource.PlayOneShot(characterSoundFx[0]);
                grunt.Play();
                // for invulnerability
                invincible = true;
                timerIsActive = true;

                Knockback(transform.position, other.transform.position);
                StartCoroutine((this.GetComponent<PlayerFlashing>().StartFlasher()));
            }
        }

        if (other.CompareTag("instantDeath"))
        {
            if (!invincible && !timerIsActive)
            {
                health = -1;
            }
        }
    }

    public void reduceHealthOfPlayer()
    {
        health--;
    }

    public void Knockback(Vector3 playerPosition, Vector3 damagingObjPosition)
    {
        knockoutTimerIsActive = true;

        if (playerPosition.x > damagingObjPosition.x)
        {
            rb.AddForce(new Vector3(horizontalForce, verticalForce, 0));

        }

        else if (playerPosition.x < damagingObjPosition.x)
        {
            rb.AddForce(new Vector3(-horizontalForce, verticalForce, 0));
        }
        this.GetComponent<CharacterController2D>().animBody.SetFloat("Speed", 0.0f);
    }

    private void respawn()
    {
        if (spawnpoint == null)
        {
            Application.LoadLevel(Application.loadedLevel);
        }
        LinkedList<GameObject> arrows = GetComponentInParent<ShootArrow>().getArrows();
        foreach (GameObject arrow in arrows)
        {
            Destroy(arrow);
        }
        arrows.Clear();

        transform.position = spawnpoint.position;
        health = numOfLives;
        GetComponentInParent<PlayerMovement>().enabled = true;
        GetComponentInParent<ShootArrow>().enabled = true;
        respawned = true;
        DeathAnim.GetComponent<Animator>().SetTrigger("Respawn");
        Invoke("removeAnim", 2);
        Debug.Log("respawning");
    }

    private void removeAnim()
    {
        SpriteRenderer[] renderers;
        renderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer renderer in renderers)
        {
            renderer.enabled = true;
        }
        DeathAnim.GetComponent<SpriteRenderer>().enabled = false;
        DeathAnim.SetActive(false);
    }
}
