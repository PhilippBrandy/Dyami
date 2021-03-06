﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatformLeft : MonoBehaviour
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
            platform.GetComponent<MovePlatformByPlayer>().moveLeft = true;
            platform.GetComponent<MovePlatformByPlayer>().moveRight = false;
            platform.GetComponent<MoveToStart>().isPFMovingLeft(true);
            platform.GetComponent<MoveToStart>().isPFMovingRight(false);
            StartCoroutine(StartPlatform());
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        player.transform.parent = null;
        platform.GetComponent<MovePlatformByPlayer>().moveLeft = false;
        platform.GetComponent<MoveToStart>().isPFMovingLeft(false);
        platform.GetComponent<MoveToStart>().isPFMovingRight(false);
        isMoving = false;
    }

    IEnumerator StartPlatform()
    {
        while (isMoving)
        {
            yield return StartCoroutine(platform.GetComponent<MovePlatformByPlayer>().MovePFLeftByPlayer());
        }
    }
}
