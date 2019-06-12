using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlashing : MonoBehaviour
{
    public GameObject[] playerPartsToFlash;
    public float redValue = 1.0f;
    public float greenValue = 0.0f;
    public float blueValue = 0.0f;
    public float transparency = 1.0f;
    public int flashDuration = 5;

    public IEnumerator StartFlasher()
    {
        for (int i = 0; i < flashDuration; i++)
        {
            for(int j = 0; j< playerPartsToFlash.Length; j++)
            {
                playerPartsToFlash[j].GetComponent<SpriteRenderer>().color = new Color(redValue, greenValue, blueValue, transparency);
            }

            yield return new WaitForSeconds(.1f);

            for (int j = 0; j < playerPartsToFlash.Length; j++)
            {
                playerPartsToFlash[j].GetComponent<SpriteRenderer>().color = Color.white;
            }
            yield return new WaitForSeconds(.1f);
        }
    }
}
