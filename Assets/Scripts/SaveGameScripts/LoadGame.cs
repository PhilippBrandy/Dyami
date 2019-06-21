using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadGame : MonoBehaviour
{
    private SaveGame[] saveGames;

    public Button button1;
    public Button button2;
    public Button button3;
    public TMPro.TextMeshProUGUI button1Text;
    public TMPro.TextMeshProUGUI button2Text;
    public TMPro.TextMeshProUGUI button3Text;

    public void Start()
    {
        saveGames = SaveGameManager.LoadSavedGames();
        button1.enabled = false;
        button2.enabled = false;
        button3.enabled = false;

        if (saveGames[0] != null)
        {
            button1.enabled = true;
            button1Text.text = saveGames[0].Date + " " + saveGames[0].CurrentScene + " Collected Items: " + saveGames[0].ItemsCollected + " / " + saveGames[0].ItemsToCollect;
        }
        else
        {
            button1.enabled = false;
        }
        if (saveGames[1] != null)
        {
            button2.enabled = true;
            button2Text.text = saveGames[1].Date + " " + saveGames[1].CurrentScene + " Collected Items: " + saveGames[0].ItemsCollected + " / " + saveGames[0].ItemsToCollect;
        }
        else
        {
            button2.enabled = false;
        }
        if (saveGames[2] != null)
        {
            button3.enabled = true;
            button3Text.text = saveGames[2].Date + " " + saveGames[2].CurrentScene + " Collected Items: " + saveGames[0].ItemsCollected + " / " + saveGames[0].ItemsToCollect;
        }
        else
        {
            button3.enabled = false;
        }
    }

    void OnGUI()
    {
        GUI.skin.button.wordWrap = true;
    }

    public void LoadCurGame(int index)
    {
        GameManager.instance.SetPlayerPos(saveGames[index].SavePosition);
        GameManager.instance.SetIsGameLoaded(true);
        SceneManager.LoadScene(saveGames[index].CurrentScene);
    }
}
