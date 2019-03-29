using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalldownTree : MonoBehaviour {

    private Rigidbody2D[] childrenPhysix;

    public ParticleSystem BreakApartFX;
    public string comparatorTag;
    private bool falling;
    private IEnumerator coroutine;

    private void Awake()
    {
        falling = false;
        childrenPhysix = GetComponentsInChildren<Rigidbody2D>();
        foreach (Rigidbody2D rigidBody in childrenPhysix)
            {
            rigidBody.bodyType = RigidbodyType2D.Static;
            }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(comparatorTag))
        {
            Instantiate(BreakApartFX, gameObject.transform);
            foreach (Rigidbody2D rigidBody in childrenPhysix)
            {
                rigidBody.bodyType = RigidbodyType2D.Dynamic;
            }
            falling = true;
        }
    }

    private void Update()
    {
        if (falling == true)
        {
            coroutine = WaitAndPrint(10.0f);
            StartCoroutine(coroutine);
        }
    }

    private IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        gameObject.SetActive(false);
    }


}
