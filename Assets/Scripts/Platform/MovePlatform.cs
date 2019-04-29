using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    // target to move the PF while moving from left to right
    public Transform target;
    // target where PF should stop after player gets near
    public Transform endTarget;
    // player can move the PF between this two targets while standing on it
    public Transform moveLeftTarget;
    public Transform moveRightTarget;

    // speed of PF while moving from left to right
    public float speedOfPF = 10.0f;
    // speed of PF while moving away from player
    public float speedOfPFMovingAwayFromPlayer = 10.0f;
    // distance from PF to endTarget
    float magnitude = 0.3f;

    public bool moveRight = false;
    public bool moveLeft = false;

    Vector3 pointB;
    bool keepMoving = true;
    float time = 3.0f;
    bool wasPFStopped = false;
    bool hasPFReachedEndPos = false;

    IEnumerator Start()
    {
        pointB = target.position;
        var pointA = transform.position;

        while (!wasPFStopped)
        {
            yield return StartCoroutine(MovePFBackAndForth(transform, pointA, pointB, time));
            yield return StartCoroutine(MovePFBackAndForth(transform, pointB, pointA, time));
        }

        if (wasPFStopped)
        {
            while (!hasPFReachedEndPos)
            {
                yield return StartCoroutine(MovePFAwayFromPlayer());
            }
        }
    }

    IEnumerator MovePFBackAndForth(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        var i = 0.0f;
        var rate = 1.0f / time;
        while (i < 1.0f && !wasPFStopped)
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos, endPos, i);
            yield return new WaitForEndOfFrame();
        }
    }

    public void StopPFFromMovingFromLeftToRight()
    {
        keepMoving = false;
        wasPFStopped = true;
    }

    public IEnumerator MovePFRightByPlayer()
    {
        while (Vector3.Distance(this.transform.position, moveRightTarget.position) > magnitude && moveRight)
        {
            float step = speedOfPF * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(this.transform.position, moveRightTarget.position, step);
            yield return 0;
        }

        // movement with acceleration
        //var i = 0.0f;
        //var rate = 0.01f / time;
        //while (i < 1.0f && moveRight)
        //{
        //    i += Time.deltaTime * rate;
        //    this.transform.position = Vector3.Lerp(transform.position, moveRightTarget.position, i);
        //    yield return null;
        //}
    }

    public IEnumerator MovePFLeftByPlayer()
    {
        while (Vector3.Distance(this.transform.position, moveLeftTarget.position) > magnitude && moveLeft)
        {
            float step = speedOfPF * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(this.transform.position, moveLeftTarget.position, step);
            yield return 0;
        }
    }

    IEnumerator MovePFAwayFromPlayer()
    {
        while (Vector3.Distance(this.transform.position, endTarget.position) > magnitude && !hasPFReachedEndPos)
        {
            float step = speedOfPFMovingAwayFromPlayer * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(this.transform.position, endTarget.position, step);
            yield return 0;
        }
        this.transform.position = endTarget.position;
        hasPFReachedEndPos = true;
    }
}
