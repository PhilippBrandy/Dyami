﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentParticleSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<ParticleSystem>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
