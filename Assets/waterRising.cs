using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterRising : MonoBehaviour
{
    public Animator animatorWater;
    public Animator TreesReset;
    public GameObject waterfall;
    public string triggerName;
    // triggerNames:
    //"Dashed" water starts to rise
    //"reset" water set to default position after rising

    // only if the player revives at checkpoint
    void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("enter:"+triggerName);
        //nur wenn der spieler beim savepoint wiederbelebt wird soll triggeranimation mit reset akiviert werden
        if (collision.CompareTag("Player")&& triggerName=="reset")
        {
            Debug.Log("enter:" + collision.name);
           // animatorWater.StopPlayback();
            triggerAnimation(); 
        }  
    }
    // Update is called once per frame
    public void triggerAnimation()
    {
        Debug.Log("triggerAnimation:"+triggerName);
        animatorWater.SetTrigger(triggerName);
        waterfall.SetActive (false);
        //wenn der spieler beimsavepoint wiederbelebt wird soll der waterfall und der tree wieder resetet werden
        if (triggerName == "reset")
        {
            waterfall.SetActive(true);
            Debug.Log("trigger reset:" + TreesReset.name);
            TreesReset.SetTrigger("reset");
        }
    }
}