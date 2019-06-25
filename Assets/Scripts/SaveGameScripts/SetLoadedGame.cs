using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetLoadedGame : MonoBehaviour
{
    public GameObject player;
    public string nameOfSecondLevel = "wood level blocking";

    private void Update()
    {
        if (GameManager.instance.IsGameLoaded())
        {
            player.transform.position = GameManager.instance.GetPlayerPos();
            player.GetComponent<ShootArrow>().learnedTeleporting = GameManager.instance.CanPlayerTeleport();

            if (SceneManager.GetActiveScene().name.Contains(nameOfSecondLevel))
            {
                player.GetComponent<ShootArrow>().learnedTeleporting = true;
            }
            GameManager.instance.SetIsGameLoaded(false);
        }
    }
}
