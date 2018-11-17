using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateRelated;

public class Guard : MonoBehaviour {

    public bool switchState = false;

    public bool isTriggered = false;
    public Transform player;
    public float triggerDistance;
    
    public float detDistance;
    public Transform groundDetection;

    //Beschleunigung
    public float Speed;
    public float Beschleunigung;

    public GuardStateMachine<Guard> guardStateMachine { get; set; }

    private void Start()
    {
        guardStateMachine = new GuardStateMachine<Guard>(this);
        guardStateMachine.ChangeState(IdleState.Instance);
        
    }

    private void Update()
    {
        if ((player.position - transform.position).magnitude < triggerDistance)
        {
            Debug.Log((player.position - transform.position).magnitude);
            isTriggered = true;
        }

        else
        {
            isTriggered = false;
        }
        guardStateMachine.Update();
    }
}
