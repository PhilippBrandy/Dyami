using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableEnvi : MonoBehaviour {
    public GameObject breakableObj;
    public Animator destroyAnimation;
    private float waitTime;

    private void Start()
    {
        waitTime = 1.0f;
    }

    public void triggerBreaking()
    {
        StartCoroutine(wait());
        destroyAnimation.SetTrigger("break");
    }


    IEnumerator wait()
    {
        yield return new WaitForSeconds(waitTime);
        breakableObj.SetActive(false);
    }
}
