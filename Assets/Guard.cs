using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateRelated;

public class Guard : MonoBehaviour {

    public bool switchState = false;

    public bool isTriggered = false;
    public Transform player;
    public float triggerDistance;
    public bool isAttacking = false;

    //attack
    public float attackDistance;
    public Transform attackDetection;
    
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
        guardStateMachine.Update();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isTriggered == true && !other.gameObject.CompareTag("ground"))
        {
            guardStateMachine.ChangeState(IdleState.Instance);
        }
    }
}
