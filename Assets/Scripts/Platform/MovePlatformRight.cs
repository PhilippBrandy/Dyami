using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatformRight : MonoBehaviour
{
    public GameObject platform;
    public GameObject player;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {

            player.transform.parent = platform.transform;
            
            platform.GetComponent<MovePlatform>().MovePlatformRight();
            platform.GetComponent<MovePlatform>().moveRight = true;
            platform.GetComponent<MovePlatform>().moveLeft = false;
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
