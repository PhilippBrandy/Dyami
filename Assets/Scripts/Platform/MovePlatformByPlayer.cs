using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatformByPlayer : MonoBehaviour
{
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


    public IEnumerator MovePFRightByPlayer()
    {
        while (Vector3.Distance(this.transform.position, moveRightTarget.position) > magnitude && moveRight)
        {
            float step = speedOfPF * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(this.transform.position, moveRightTarget.position, step);
            yield return 0;
        }
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
}
