using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatformLeft : MonoBehaviour
{
    public GameObject platform;
    public GameObject player;

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.CompareTag("Player"))
        {
            player.transform.parent = platform.transform;
            platform.GetComponent<MovePlatform>().MovePlatformLeft();
            platform.GetComponent<MovePlatform>().moveLeft = true;
            platform.GetComponent<MovePlatform>().moveRight = false;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            player.transform.parent = null;
        }
    }
}
