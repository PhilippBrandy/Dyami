using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchToCrawling : MonoBehaviour
{
    public Animator mainAnimator;
    public Animator crawlAnimator;
    public GameObject crawlingRig;
    public GameObject armsRig;
    public GameObject headRig;
    public GameObject bodyRig;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        crawlingRig.SetActive(true);
        armsRig.SetActive(false);
        headRig.SetActive(false);
        SpriteRenderer[] sprites = bodyRig.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer current in sprites)
        {
            current.enabled = false;
        }
        crawlAnimator.SetBool("crawling", true);
    }

    private void Update()
    {
        crawlAnimator.SetFloat("Speed", mainAnimator.GetFloat("Speed"));
    }
}
