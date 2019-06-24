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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("falling"))
        {
            gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            triggerBreaking();
        }
    }

    public void triggerBreaking()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        StartCoroutine(wait());
        destroyAnimation.SetTrigger("break");
    }


    IEnumerator wait()
    {
        yield return new WaitForSeconds(waitTime);
        breakableObj.SetActive(false);
    }
}
