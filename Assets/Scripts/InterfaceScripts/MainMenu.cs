﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene("Spirit Cave");
    }

    public void LoadGameUI()
    {
        SceneManager.LoadScene("LoadMenu");
    }


    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
