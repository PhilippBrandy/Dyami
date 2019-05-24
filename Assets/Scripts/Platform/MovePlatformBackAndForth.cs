using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatformBackAndForth : MonoBehaviour
{
    // target to move the PF while moving from left to right (= point a)
    // point b is the point where the PF is in the scene view
    public Transform target;

    public GameObject player;

    // speed of PF while moving from left to right
    public float speedOfPF = 10.0f;

    // distance from PF to endTarget
    float magnitude = 0.3f;

    Vector3 pointB;
    bool keepMoving = true;
    float time = 3.0f;
    bool wasPFStopped = false;
    bool hasPFReachedEndPos = false;

    // Fololowing code is just necessary if you want the PF moving away when the player gets near
    // target where PF should stop after player gets near
    //public Transform endTarget;

    // speed of PF while moving away from player
    //public float speedOfPFMovingAwayFromPlayer = 10.0f;


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
            // here the platform moves away from the player
            //while (!hasPFReachedEndPos)
            //{
            //    yield return StartCoroutine(MovePFAwayFromPlayer());
            //}
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

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            player.transform.parent = this.transform;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            player.transform.parent = null;
        }
    }

    // if the PF should be stopped by a trigger
    // this could be called from another gameObject-Script like StopPlattformByTrigger
    public void StopPFFromMovingFromLeftToRight()
    {
        keepMoving = false;
        wasPFStopped = true;
    }

    //IEnumerator MovePFAwayFromPlayer()
    //{
    //    while (Vector3.Distance(this.transform.position, endTarget.position) > magnitude && !hasPFReachedEndPos)
    //    {
    //        float step = speedOfPFMovingAwayFromPlayer * Time.deltaTime;
    //        this.transform.position = Vector3.MoveTowards(this.transform.position, endTarget.position, step);
    //        yield return 0;
    //    }
    //    this.transform.position = endTarget.position;
    //    hasPFReachedEndPos = true;
    //}
}
