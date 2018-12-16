using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slowingdown : State
{
    private Vector3 direction;

    public override void EnterState(BearAI owner)
    {
        if (owner.charging.directionLoR == -1)
        {
            direction = Vector3.left;
        }
        else
        {
            direction = Vector3.right;
        }
    }

    public override void ExitState(BearAI owner)
    {
        direction = new Vector3(0, 0, 0);
    }

    public override void UpdateState(BearAI owner)
    {
        // wenn minimale Geschwindigkeit noch nicht erreicht ist, werde langsamer
        if (owner.currentSpeed > 0)
        {
            owner.currentSpeed -= 2 * owner.acceleration * Time.deltaTime;
        }
        if (owner.currentSpeed < 0)
        {
            owner.currentSpeed = 0;
        }

        //Schau ob vor dir Boden oder eine Wand ist
        RaycastHit2D groundInfo = Physics2D.Raycast(owner.groundDetection.position, Vector2.down, owner.detectionRange, 0);
        RaycastHit2D wallInfo = Physics2D.Raycast(owner.wallDetection.position, owner.wallDetection.transform.right, owner.detectionRange, 0);

        if (groundInfo.collider == null)
        {
            owner.currentSpeed = 0;
        }
        else if (wallInfo.collider != null)
        {
            owner.currentSpeed = 0;
        }

        //Beweg dich je nach Geschwindigkeit
        owner.gameObject.transform.position += direction * owner.currentSpeed * Time.deltaTime;
    }
}
