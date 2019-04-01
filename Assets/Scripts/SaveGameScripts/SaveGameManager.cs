﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveGameManager
{
    public static void AddSaveGame(SaveGame saveGame)
    {
        SaveGame[] saveGames = LoadSavedGames();

        for (int i = saveGames.Length - 1; i > 0; i--)
        {
            saveGames[i] = saveGames[i - 1];
        }

        saveGames[0] = saveGame;

        for (int i = 0; i < saveGames.Length; i++)
        {
            if (saveGames[i] != null)
            {
                SaveGame(saveGames[i], i);
            }
        }
    }

    public static SaveGame[] LoadSavedGames()
    {
        SaveGame[] saveGames = new SaveGame[3];

        for (int i = 0; i < saveGames.Length; i++)
        {
            saveGames[i] = LoadSavedGame(i);
        }

        return saveGames;
    }

    private static void SaveGame(SaveGame saveGame, int index)
    {
        PlayerPrefs.SetFloat("PlayerX" + index, saveGame.SavePosition.x);
        PlayerPrefs.SetFloat("PlayerY" + index, saveGame.SavePosition.y);
        PlayerPrefs.SetFloat("PlayerZ" + index, saveGame.SavePosition.z);
        PlayerPrefs.SetString("Date" + index, saveGame.Date);
    }

    private static SaveGame LoadSavedGame(int gameIndex)
    {
        SaveGame saveGame = new SaveGame();
        string date = PlayerPrefs.GetString("Date" + gameIndex, null);

        if (date == null)
        {
            return null;
        }

        saveGame.Date = date;
        saveGame.SavePosition = new Vector3(PlayerPrefs.GetFloat("PlayerX" + gameIndex), PlayerPrefs.GetFloat("PlayerY" + gameIndex), PlayerPrefs.GetFloat("PlayerZ" + gameIndex));

        return saveGame;
    }
}