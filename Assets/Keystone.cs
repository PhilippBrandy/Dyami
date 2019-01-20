using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keystone : MonoBehaviour
{
    public GameObject activeEyes;
    public GameObject notactiveEyes;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponentInChildren<ShootArrow>().theForce == true)
            {
                activeEyes.SetActive(true);
                notactiveEyes.SetActive(false);
            }
        }
    }
}
