using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashableObjectForWater : MonoBehaviour
{
    public string triggerName;
    private Animator animator;
    bool waterTrigger;
    // Start is called before the first frame update
    void Start()
    {
        waterTrigger = false;
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    public void triggerAnimation()
    {
        animator.SetTrigger(triggerName);
        waterTrigger = true;
    }
}
