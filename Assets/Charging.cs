using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charging : State
{
    public int directionLoR;
    private Vector3 direction;

    //wennst die State betrittst, schau ob der Spieler links oder rechts ist und dreh dich in seine Richtung
    public override void EnterState(BearAI owner)
    {
        Debug.Log("entered Triggered");
        if (owner.player.transform.position.x < owner.transform.position.x)
        {
            owner.transform.eulerAngles = new Vector3(0, -180, 0);
            direction = Vector3.left;
            directionLoR = -1;
        }
        else
        {
            owner.transform.eulerAngles = new Vector3(0, 0, 0);
            direction = Vector3.right;
            directionLoR = 1;
        }
    }



    public override void ExitState(BearAI owner)
    {
        Debug.Log("exiting Triggered");
        direction = new Vector3(0, 0, 0);
    }



    public override void UpdateState(BearAI owner)
    {
        Debug.Log("updating Triggered");
        // wenn maximale Geschwindigkeit noch nicht erreicht ist, werde schneller
        if (owner.currentSpeed < owner.maxSpeed)
        {
            owner.currentSpeed += owner.acceleration * Time.deltaTime;
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
