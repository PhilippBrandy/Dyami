using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterRising : MonoBehaviour
{
    public Animator animatorWater;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");

        animatorWater = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    public void triggerAnimation()
    {
        Debug.Log("triggerwater");
        animatorWater.SetTrigger("Dashed");
    }
}