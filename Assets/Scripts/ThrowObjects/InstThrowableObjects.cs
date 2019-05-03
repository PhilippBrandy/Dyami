using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstThrowableObjects : MonoBehaviour
{
    public GameObject prefab;
    public GameObject curObject;
    public Transform spawnpoint;
    public float seconds = 5.0f;
    public float startTime = 3.0f;

    // to stop throwing by another script
    public bool stopSpawning = false;

    void Start()
    {
        InvokeRepeating("SpawnObjects", startTime, seconds);
    }

    public void SpawnObjects()
    {
        curObject = Instantiate(prefab, spawnpoint.transform.position, Quaternion.identity);
        if (stopSpawning)
        {
            // stop throwing the objects
        }
    }
}
