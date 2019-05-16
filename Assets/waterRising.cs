using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterRising : MonoBehaviour
{
    public Animator animatorWater;
    public GameObject waterfall;
    public string triggerName;
    // triggerNames:
    //"Dashed" water starts to rise
    //"reset" water set to default position after rising
    //

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("swag?" + triggerName);

        animatorWater = gameObject.GetComponent<Animator>();
    }

    

    // Update is called once per frame
    public void triggerAnimation()
    {
        Debug.Log(""+triggerName);
       
        animatorWater.SetTrigger(triggerName);
        waterfall.SetActive (false);
        if (triggerName == "reset")
        {
            waterfall.SetActive(true);

        }
    }
}