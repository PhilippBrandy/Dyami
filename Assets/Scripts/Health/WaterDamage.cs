using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDamage : MonoBehaviour
{
    // script has to be assigned to the damaging water

    public float secondsInvulnerableInWater = 3f;
    float waterTimer = 0.0f;
    bool waterTimerIsActive = false;
    bool playerIsInWater = false;
    public GameObject player;

    void Update()
    {
        if (waterTimerIsActive && playerIsInWater)
        {
            waterTimer += Time.deltaTime;
            if (waterTimer >= secondsInvulnerableInWater)
            {
                waterTimerIsActive = false;
                waterTimer = 0.0f;
                player.GetComponent<Killable>().reduceHealthOfPlayer();
                waterTimerIsActive = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!playerIsInWater && !waterTimerIsActive)
            {
                waterTimerIsActive = true;
                playerIsInWater = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInWater = false;
        }
    }
}
