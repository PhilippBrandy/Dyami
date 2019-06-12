using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;

public class PauseMenu : MonoBehaviour
{

    public static bool gameIsPause = false;
    public GameObject pauseMenuUI;
    public TMPro.TextMeshProUGUI gameIsPausedText;
    public GameObject settingsMenuUI;
    bool shouldPause = true;

    // for pausing the music in the menu
    public GameObject audioObject;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && shouldPause)
        {
                Pause();
        }
    }

    public void Pause()
    {
        audioObject.GetComponent<AudioSource>().enabled = false;
        pauseMenuUI.SetActive(true);
        gameIsPausedText.enabled = true;
        Time.timeScale = 0f;
        gameIsPause = true;
    }

    public void Resume()
    {
        audioObject.GetComponent<AudioSource>().enabled = true;
        pauseMenuUI.SetActive(false);
        gameIsPausedText.enabled = false;
        Time.timeScale = 1f;
        gameIsPause = false;
        shouldPause = true;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SettingsMenu()
    {
        shouldPause = false;
        pauseMenuUI.SetActive(false);
        gameIsPausedText.enabled = false;
        settingsMenuUI.SetActive(true);
        gameIsPause = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetTimeScaleActive()
    {
        Time.timeScale = 1f;
        gameIsPause = false;
    }
}
