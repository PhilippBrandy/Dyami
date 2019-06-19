using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchToWalking : MonoBehaviour
{
    public Animator mainAnimator;
    public Animator crawlAnimator;
    public GameObject crawlingRig;
    public GameObject armsRig;
    public GameObject headRig;
    public SpriteRenderer [] bodyRigSprites;
    public AnimationClip reviveAnim;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        crawlAnimator.SetTrigger("reachForTotem");
        float animDuration = reviveAnim.length - 0.1f;

        StartCoroutine(wait(animDuration));
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator wait(float sec)
    {
        yield return new WaitForSeconds(sec);
        
        foreach (SpriteRenderer current in bodyRigSprites)
        {
            current.enabled = true;
        }
        armsRig.SetActive(true);
        headRig.SetActive(true);
        crawlAnimator.SetBool("crawling", false);
        crawlingRig.SetActive(false);
        mainAnimator.SetFloat("Speed", 0.0f);
    }
}
