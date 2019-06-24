﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class sceneReload : MonoBehaviour
{
    public GameObject avatar;
    public Scene scene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (avatar.GetComponent<Killable>().health == 0)
        {
        scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);

        }

    }
}
