using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slowingdown : State
{
    public override void EnterState(BearAI owner)
    {
        
    }

    public override void ExitState(BearAI owner)
    {
        
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
        if (RAYCASTS)
        {
            owner.currentSpeed = 0;
        }

        //Beweg dich je nach Geschwindigkeit
        owner.gameObject.transform.position += direction * owner.currentSpeed * Time.deltaTime;
    }
}
