using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawnpoint : MonoBehaviour
{

    private long lastGameSave = 0;
    private int saveDelay = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Killable>().spawnpoint = gameObject.transform;

            if (lastGameSave + saveDelay < System.DateTime.Now.Millisecond)
            {
                SaveGame saveGame = new SaveGame();
                saveGame.Date = System.DateTime.Now.ToString("dd.MM.yyyy HH:mm");
                saveGame.SavePosition = gameObject.transform.position;
                saveGame.CurrentScene = SceneManager.GetActiveScene().name;
                saveGame.ItemsCollected = GameManager.instance.GetNumberOfCollectedItems();
                saveGame.ItemsToCollect = GameManager.instance.GetNumberOfAllCollectableItems();
                SaveGameManager.AddSaveGame(saveGame);
                lastGameSave = System.DateTime.Now.Millisecond;
            }
        }
    }
}
