using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StonePlatform : MonoBehaviour
{

    public GameObject platform;
    public GameObject sinkTarget;
    public GameObject origPosition;
    bool moveStoneDown = false;
    bool moveStoneUp = false;
    bool arrowEntered = false;
    bool moveStone = true;
    public float speed = 5.0f;
    float magnitude = 0.3f;
    float seconds = 7f;
    float timer = 0.0f;
    bool timerIsActive = false;
    

    void Update()
    {
        if (timerIsActive)
        {
            timer += Time.deltaTime;
            if (timer >= seconds)
            {
                timerIsActive = false;
            }
        }

        else
        {
            if (moveStone && moveStoneDown && Vector3.Distance(platform.transform.position, sinkTarget.transform.position) > magnitude)
            {
                float step = speed * Time.deltaTime;
                platform.transform.position = Vector3.MoveTowards(platform.transform.position, sinkTarget.transform.position, step);
            }

            if (moveStone && Vector3.Distance(platform.transform.position, origPosition.transform.position) > magnitude && moveStoneUp)
            {
                float step = speed * Time.deltaTime;
                platform.transform.position = Vector3.MoveTowards(platform.transform.position, origPosition.transform.position, step);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && moveStone)
        {
            moveStoneDown = true;
            moveStoneUp = false;
        }
        else
        {
            timerIsActive = true;
            moveStone = false;
            moveStone = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {

        if (col.CompareTag("Player") && moveStone)
        {
            moveStoneUp = true;
            moveStoneDown = false;
        }
    }
}
