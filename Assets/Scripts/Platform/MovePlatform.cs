using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    Vector3 pointB;
    public Transform target;
    public Transform endTarget;
    public Collider2D collider;
    bool keepMoving = true;
    float time = 3.0f;
    float speed = 10.0f;
    public Transform moveLeftTarget;
    public Transform moveRightTarget;
    public bool moveRight = false;
    public bool moveLeft = false;

    IEnumerator Start()
    {
        pointB = target.position;
        var pointA = transform.position;
        while (true)
        {
            yield return StartCoroutine(MoveObject(transform, pointA, pointB, time));
            yield return StartCoroutine(MoveObject(transform, pointB, pointA, time));
        }
    }

    IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        if (keepMoving)
        {
            var i = 0.0f;
            var rate = 1.0f / time;
            while (i < 1.0f)
            {
                i += Time.deltaTime * rate;
                thisTransform.position = Vector3.Lerp(startPos, endPos, i);
                yield return null;
            }
        }
        else
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, endTarget.position, step);
        }
    }

    public void StopPlatform()
    {
        keepMoving = false;
    }

    public IEnumerator MovePlatformRight()
    {
        
        Debug.Log("moveRight");
        while (Vector3.Distance(this.transform.position, moveLeftTarget.position) > speed)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, moveRightTarget.position, speed);
            yield return 0;
        }

        this.transform.position = moveRightTarget.position;


        //float step = speed * Time.deltaTime;
        //    transform.position = Vector3.MoveTowards(transform.position, moveRightTarget.position, step);
    }

    public IEnumerator MovePlatformLeft()
    {
        Debug.Log("moveLeft");
        while (Vector3.Distance(this.transform.position, moveLeftTarget.position) > speed)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, moveLeftTarget.position, speed);
            yield return 0;
        }

        this.transform.position = moveLeftTarget.position;
        
        //float step = speed * Time.deltaTime;
        //transform.position = Vector3.MoveTowards(transform.position, moveLeftTarget.position, step);
    }

    IEnumerator MoveToTarget()
    {
        var i = 0.0f;
        var rate = 1.0f / time;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.position = Vector3.Lerp(transform.position, pointB, i);
            yield return null;
        }
    }

}
