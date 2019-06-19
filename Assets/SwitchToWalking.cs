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
    public GameObject bodyRig;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        crawlAnimator.SetTrigger("reachForTotem");
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
