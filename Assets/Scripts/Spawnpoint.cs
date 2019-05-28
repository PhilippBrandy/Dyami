using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint : MonoBehaviour
{

    private long lastGameSave = 0;
    public Killable player;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
            if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("e pressed");
            player.health = 0;
            
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Killable>().spawnpoint = gameObject.transform;

            if (lastGameSave + 5000 < System.DateTime.Now.Millisecond)
            {
                SaveGame saveGame = new SaveGame();
                saveGame.Date = System.DateTime.Now.ToString("dd.MM.yyyy HH:mm");
                saveGame.SavePosition = gameObject.transform.position;
                SaveGameManager.AddSaveGame(saveGame);

                lastGameSave = System.DateTime.Now.Millisecond;
            }
        }
    }
}
