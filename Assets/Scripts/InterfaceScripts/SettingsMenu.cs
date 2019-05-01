using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    Resolution[] resolutions;
    public Dropdown resolutionDropdown;
    public GameObject pauseMenuUI;
    public TMPro.TextMeshProUGUI gameIsPausedText;
    public GameObject settingsMenuUI;

    // Keybinding
    public Transform keyBindingGroup;
    bool waitingForKey;
    Event keyEvent;
    KeyCode newKey;
    Text buttonText;


    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int curResIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                curResIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = curResIndex;
        resolutionDropdown.RefreshShownValue();

        // KEYBINDING
        //waitingForKey = false;
        //for (int i = 0; i < keyBindingGroup.childCount; i++)
        //{
        //    if (keyBindingGroup.GetChild(i).name == "left" && GameManager.instance != null)
        //    {
        //        keyBindingGroup.GetChild(i).GetComponentInChildren<Text>().text = GameManager.instance.left.ToString();
        //    }
        //    else if (keyBindingGroup.GetChild(i).name == "right" && GameManager.instance != null)
        //    {
        //        keyBindingGroup.GetChild(i).GetComponentInChildren<Text>().text = GameManager.instance.right.ToString();
        //    }
        //    else if (keyBindingGroup.GetChild(i).name == "jump" && GameManager.instance != null)
        //    {
        //        keyBindingGroup.GetChild(i).GetComponentInChildren<Text>().text = GameManager.instance.jump.ToString();
        //    }
        //}
    }
    
    // ------------------------ RESOLUTION, GRAPHICS, AUDIO ------------------------------------
    
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex, true);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void BackToMenu()
    {
        settingsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        gameIsPausedText.enabled = true;
    }

    // -------------------------------- KEYBINDING ---------------------------------------------

    //private void OnGUI()
    //{
    //    keyEvent = Event.current;
    //    if (keyEvent.isKey && waitingForKey)
    //    {
    //        newKey = keyEvent.keyCode;
    //        waitingForKey = false;
    //    }
    //}

    //public void StartAssignment(string keyName)
    //{
    //    if (!waitingForKey)
    //    {
    //        StartCoroutine(AssignKey(keyName));
    //    }
    //}

    //public void SendText(Text text)
    //{
    //    buttonText = text;
    //}

    //IEnumerator WaitForKey()
    //{
    //    while (!keyEvent.isKey)
    //        yield return null;
    //}

    //public IEnumerator AssignKey(string keyName)
    //{
    //    waitingForKey = true;
    //    yield return WaitForKey();

    //    if (GameManager.instance != null)
    //    {
    //        switch (keyName)
    //        {
    //            case "left":
    //                Debug.Log(keyName);
    //                GameManager.instance.left = newKey;
    //                buttonText.text = GameManager.instance.left.ToString();
    //                Debug.Log(buttonText.text);
    //                PlayerPrefs.SetString("left", GameManager.instance.left.ToString());
    //                break;
    //            case "right":
    //                GameManager.instance.right = newKey;
    //                buttonText.text = GameManager.instance.right.ToString();
    //                PlayerPrefs.SetString("right", GameManager.instance.right.ToString());
    //                break;
    //            case "jump":
    //                GameManager.instance.jump = newKey;
    //                buttonText.text = GameManager.instance.jump.ToString();
    //                PlayerPrefs.SetString("jump", GameManager.instance.jump.ToString());
    //                break;
    //        }
    //        yield return null;
    //    }
    //}
}
