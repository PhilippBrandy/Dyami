using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearAI : MonoBehaviour {
    public static BearAI instance;

    // trigger
    public int triggerDistance;
    public int triggerHeigth;

    //movement
    public float currentSpeed;
    public float maxSpeed;
    public float acceleration;
    public Transform player;

    //states
    private Guarding guarding;
    public Charging charging;
    private Slowingdown slowingdown;

    // raycasts
    public Transform groundDetection;
    public Transform wallDetection;
    public int detectionRange;

    public State currentState { get; private set; }


    public enum GuardState { guarding, charging, slowingdown };
    GuardState currentGuardState = GuardState.guarding;

    private void Awake()
    {
        BearAI.instance = this;
        guarding = new Guarding();
        charging = new Charging();
        slowingdown = new Slowingdown();
        currentState = guarding;
        currentSpeed = 0;
    }
	
	// Update is called once per frame
	void Update () {
        //schau ob du wechseln musst
        // wenn guarding
        if (currentGuardState == GuardState.guarding)
        {
            //wenn Spieler in Trigger-Range kommt
            if (gameObject.transform.position.x - player.position.x <= triggerDistance && gameObject.transform.position.y - player.position.y <= triggerHeigth)
            {
                setState(GuardState.charging);
            }
        }

        // wenn charging
        else if (currentGuardState == GuardState.charging)
        {
            //wenn Spieler aus Trigger-Range kommt
            if (gameObject.transform.position.x - player.position.x > triggerDistance || gameObject.transform.position.y - player.position.y > triggerHeigth)
            {
                setState(GuardState.slowingdown);
            }
            else
            {
                //wenn Spieler hinter Bär ist
                float lor = player.transform.position.x - transform.position.x;
                switch (charging.directionLoR)
                {
                    case 1:
                        if (lor < 0)
                        {
                            setState(GuardState.slowingdown);
                        }
                        break;
                    case -1:
                        if (lor > 0)
                        {
                            setState(GuardState.slowingdown);
                        }
                        break;
                }
            }
        }

        // wenn slowingdown
        else if (currentGuardState == GuardState.slowingdown)
        {
            if (currentSpeed <= 0)
            {
                setState(GuardState.guarding);
            }
        }


        // Update die jetzt eingestellte State
        currentState.UpdateState(this);
	}



    void setState(GuardState nextState)
    {
        currentGuardState = nextState;
        currentState.ExitState(this);
        switch(nextState)
        {
            case GuardState.guarding:
                currentState = guarding;
                break;
            case GuardState.charging:
                currentState = charging;
                break;
            case GuardState.slowingdown:
                currentState = slowingdown;
                break;
        }
        currentState.EnterState(this);
    }
}



//Abstrakte Klasse für die einzelnen STATES
public abstract class State
{
    public abstract void EnterState(BearAI owner);
    public abstract void ExitState(BearAI owner);
    public abstract void UpdateState(BearAI owner);
}
