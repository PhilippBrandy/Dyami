using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPlatform : MonoBehaviour
{
    private bool isRotatingDown;
    private bool isRotationUp;
    public float waitTime;
    public float rotationSpeed;
    public GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        isRotatingDown = false;
        isRotatingDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRotatingDown && gameObject.transform.rotation.z <= 90)
        {
            parent.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
        else if (isRotationUp && gameObject.transform.rotation.z >= 0)
        {
            parent.transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isRotationUp == true)
            {
                isRotationUp = false;
            }
            StartCoroutine(wait());
            isRotatingDown = true;
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(wait());
            isRotationUp = true;
        }
        
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(waitTime);
    }
}
