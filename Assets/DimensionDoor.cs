using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionDoor : MonoBehaviour
{
    public Transform exit;
    public Transform player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.position = exit.position + Vector3.right;
        }
    }
}
