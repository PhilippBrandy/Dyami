using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatingHead : MonoBehaviour
{
    Animation floatinganimation;
    // Start is called before the first frame update
    void Start()
    {
        floatinganimation = gameObject.GetComponent<Animation>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!floatinganimation.isPlaying)
        {
            floatinganimation.Play();
        }
    }
}
