﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetOnDeath : MonoBehaviour
{
    private Killable player;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Avatar").GetComponent<Killable>();
        
        originalPosition = gameObject.transform.position;
        originalRotation = gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.health == 0)
        {
            gameObject.transform.position = originalPosition;
            gameObject.transform.rotation = originalRotation;
        }
    }
}
