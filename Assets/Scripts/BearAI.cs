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

    //animator
    private Animator animator;

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

        animator = gameObject.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        //schau ob du wechseln musst
        // wenn guarding
        if (currentGuardState == GuardState.guarding)
        {
            //wenn Spieler in Trigger-Range kommt
            if (gameObject.transform.position.x - player.position.x <= triggerDistance && Mathf.Abs(gameObject.transform.position.y - player.position.y) <= triggerHeigth)
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

        // tell the animator your current speed
        animator.SetFloat("currentSpeed", currentSpeed);
        if (currentSpeed == 0)
        {
            animator.speed = 1.0f;
        } else if (currentSpeed < 5)
        {
            animator.speed = 0.5f;
        } else if (currentSpeed >= 5 && currentSpeed < 20)
        {
            animator.speed = 1.0f;
        } else if (currentSpeed >= 50)
        {
            animator.speed = 1.5f;
        }
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
