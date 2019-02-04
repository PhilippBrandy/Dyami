﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keystone : MonoBehaviour
{
    public RiddleManager owner;
    public GameObject activeEyes;
    public GameObject notactiveEyes;
    public bool activated;
    public string key;

    private void Start()
    {
        activated = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && activated == false)
        {
            if (other.GetComponentInChildren<ShootArrow>().theForce == true)
            {
                activeEyes.SetActive(true);
                notactiveEyes.SetActive(false);
                activated = true;
                owner.collectedPassword += key;
            }
        }
    }

    public void deactivate()
    {
        activeEyes.SetActive(false);
        notactiveEyes.SetActive(true);
        activated = false;
    }
}
