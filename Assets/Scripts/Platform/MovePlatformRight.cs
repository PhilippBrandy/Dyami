using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatformRight : MonoBehaviour
{
    public GameObject platform;
    public GameObject player;
    bool isMoving = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !isMoving)
        {
            player.transform.parent = platform.transform;
            isMoving = true;
            platform.GetComponent<MovePlatform>().moveRight = true;
            platform.GetComponent<MovePlatform>().moveLeft = false;
            StartCoroutine(StartPlatform());
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            player.transform.parent = null;
            platform.GetComponent<MovePlatform>().moveRight = false;
            isMoving = false;
        }
    }
    IEnumerator StartPlatform()
    {
        while (isMoving)
        {
            yield return StartCoroutine(platform.GetComponent<MovePlatform>().MovePFRightByPlayer());
        }
    }
}
