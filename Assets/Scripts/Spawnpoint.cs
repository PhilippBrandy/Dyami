using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawnpoint : MonoBehaviour
{
    GameObject checkpointManager;
    string checkpointsReached;

    private void Start()
    {
        checkpointManager = GameObject.Find("CheckpointManager");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Killable>().spawnpoint = gameObject.transform;

            checkpointsReached = checkpointManager.GetComponent<CheckpointManager>().GetNamesOfReachedCheckpoints();

            if (!checkpointsReached.Contains(this.gameObject.name))
            {
                SaveGame saveGame = new SaveGame();
                saveGame.Date = System.DateTime.Now.ToString("dd.MM.yyyy HH:mm");
                saveGame.SavePosition = gameObject.transform.position;
                saveGame.CurrentScene = SceneManager.GetActiveScene().name;
                if (GameManager.instance != null)
                {
                    saveGame.ItemsCollected = GameManager.instance.GetNumberOfCollectedItems();
                    saveGame.ItemsToCollect = GameManager.instance.GetNumberOfAllCollectableItems();
                    SaveGameManager.AddSaveGame(saveGame);
                    checkpointManager.GetComponent<CheckpointManager>().AddReachedCheckpoint(this.gameObject.name);
                }
            }
        }
    }
}
