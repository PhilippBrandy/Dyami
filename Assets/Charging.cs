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
        directionLoR = 0;
    }



    public override void UpdateState(BearAI owner)
    {
        // wenn maximale Geschwindigkeit noch nicht erreicht ist, werde schneller
        if (owner.currentSpeed < owner.maxSpeed)
        {
            owner.currentSpeed += owner.acceleration * Time.deltaTime;
        }

        //Schau ob vor dir Boden oder eine Wand ist
        if (RAYCASTS)
        {
            owner.currentSpeed = 0;
        }

        //Beweg dich je nach Geschwindigkeit
        owner.gameObject.transform.position += direction * owner.currentSpeed * Time.deltaTime;
    }
}
