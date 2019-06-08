using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetItemCanvas : MonoBehaviour
{
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.GetComponent<CollectItems>().SetItemsCollectedNumber(0);
            player.GetComponent<CollectItems>().SetItemsCanvasInvisible();
        }
    }
}
