using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateRelated;

public class TriggeredState : State<Guard>
{
    private static TriggeredState _instance;
    private Transform target;

    //Beschleunigung und nur links/rechts
    
    private Vector3 direction;
    private float currentSpeed = 0;

    public Transform groundDetection;

    private Vector2 tmpVec;

    private TriggeredState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static TriggeredState Instance
    {
        get
        {
            if (_instance == null)
            {
                new TriggeredState();
            }

            return _instance;
        }
    }

    public override void EnterState(Guard _owner)
    {
        Debug.Log("Entering Triggered State");

        target = _owner.player;
    }

    public override void ExitState(Guard _owner)
    {
        Debug.Log("Exiting Triggered State");
        currentSpeed = 0f;
    }

    public override void UpdateState(Guard _owner)
    {
        if (!_owner.isTriggered)
        {
            currentSpeed = 0f;
            _owner.guardStateMachine.ChangeState(IdleState.Instance);
        }

        // schau ob Player links is, ansonsten, schau ob Player rechts is

        //ist links
        if (target.transform.position.x < _owner.transform.position.x)
        {
            _owner.transform.eulerAngles = new Vector3(0, -180, 0);
            direction = Vector3.left;
            charge(_owner);
        }
        //ist rechts
        else
        {
            _owner.transform.eulerAngles = new Vector3(0, 0, 0);
            direction = Vector3.right;
            charge(_owner);
        }
    }

    private void charge(Guard _owner)
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(_owner.groundDetection.position, Vector2.down, _owner.detDistance);

        if (groundInfo.collider == true)
        {
            if (currentSpeed < _owner.Speed)
            {
                currentSpeed += _owner.Beschleunigung * Time.deltaTime;
            }
            _owner.transform.position += direction * currentSpeed * Time.deltaTime;


            //_owner.transform.position = Vector2.MoveTowards(_owner.transform.position, target.position, _owner.speed * Time.deltaTime);
        }
        else
        {
            currentSpeed = 0f;
        }
    }
    
}
