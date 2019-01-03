using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateParrallaxEffect : MonoBehaviour
{
    private ScrollingBackground toActivate;
    private bool isActive;
    private SpriteRenderer[] spritesArray;
    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        spritesArray = gameObject.GetComponentsInChildren<SpriteRenderer>();
        toActivate = gameObject.GetComponent<ScrollingBackground>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive == false)
        {
            foreach (SpriteRenderer spriteRendered in spritesArray)
            {
                if (spriteRendered.isVisible == true)
                {
                    toActivate.enabled = true;
                }
            }
        }
        else
        {
            foreach (SpriteRenderer spriteRendered in spritesArray)
            {
                bool nonVisible = true;
                if (spriteRendered.isVisible == true)
                {
                    nonVisible = false;
                }
                if (nonVisible == true)
                {
                    toActivate.enabled = false;
                }
            }
        }
    }
}
