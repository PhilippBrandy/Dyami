using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterRising : MonoBehaviour
{
    public string triggerName;
    public Animator animator;
    public Animator animatorWater;

    // Start is called before the first frame update
    void Start()
    {
       

        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    public void triggerAnimation()
    {

        animator.SetTrigger(triggerName);
        animatorWater.SetTrigger(triggerName);
    }
}