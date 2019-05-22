using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashableObject : MonoBehaviour
{
    public string triggerName;
    private Animator animator;
    public bool letWaterRise;
    public waterRising trigger;

    // Start is called before the first frame update
    void Start()
    {
        
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    public void triggerAnimation()
    {
        if (letWaterRise == true) { 
            trigger.triggerAnimation();
       }
        animator.SetTrigger(triggerName);
        
    }
}
