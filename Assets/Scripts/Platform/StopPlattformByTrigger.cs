using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPlattformByTrigger : MonoBehaviour
{
    public GameObject platform;

    void OnTriggerEnter2D(Collider2D col)
    {
        platform.GetComponent<MovePlatform>().StopPFFromMovingFromLeftToRight();
    }
}
