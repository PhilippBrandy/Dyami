using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    private SaveGame[] saveGames;

    public Button button1;
    public Button button2;
    public Button button3;

    public void Start()
    {
        saveGames = SaveGameManager.LoadSavedGames();
        button1.enabled = false;
        button2.enabled = false;
        button3.enabled = false;

        if (saveGames[0] != null)
        {
            button1.enabled = true;
        }
        else
        {
            button1.enabled = false;
        }
        if (saveGames[1] != null)
        {
            button2.enabled = true;
        }
        else
        {
            button2.enabled = false;
        }
        if (saveGames[2] != null)
        {
            button3.enabled = true;
        }
        else
        {
            button3.enabled = false;
        }
    }

    public void LoadCurGame(int index)
    {
        GameManager.instance.SetPlayerPos(saveGames[index].SavePosition);
        SceneManager.LoadScene("TUTORIAL+1LVL");
    }
}
