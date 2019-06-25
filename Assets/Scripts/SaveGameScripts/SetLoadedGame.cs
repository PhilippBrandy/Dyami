using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLoadedGame : MonoBehaviour
{
    public GameObject player;

    private void Update()
    {
        if (GameManager.instance.IsGameLoaded())
        {
            player.transform.position = GameManager.instance.GetPlayerPos();
            player.GetComponent<ShootArrow>().learnedTeleporting = GameManager.instance.CanPlayerTeleport();
            GameManager.instance.SetIsGameLoaded(false);
        }
    }
}
