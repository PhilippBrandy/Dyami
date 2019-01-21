using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportGate : MonoBehaviour
{
    public GameObject exitGate;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.transform.position = exitGate.transform.position;
    }
}
