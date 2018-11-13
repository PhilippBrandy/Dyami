using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateRelated;

public class TriggeredState : State<Guard>
{
    private static TriggeredState _instance;
    private Transform target;

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
    }

    public override void UpdateState(Guard _owner)
    {
        if (!_owner.isTriggered)
        {
            _owner.guardStateMachine.ChangeState(IdleState.Instance);
        }

        // schau ob Player links is, ansonsten, schau ob Player rechts is

        //ist links
        if (target.transform.position.x < _owner.transform.position.x)
        {
            _owner.transform.eulerAngles = new Vector3(0, -180, 0);
            charge(_owner);
        }
        //ist rechts
        else
        {
            _owner.transform.eulerAngles = new Vector3(0, 0, 0);
            charge(_owner);
        }
    }

    private void charge(Guard _owner)
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(_owner.groundDetection.position, Vector2.down, _owner.detDistance);

        if (groundInfo.collider == true)
        {
            _owner.transform.position = Vector2.MoveTowards(_owner.transform.position, target.position, _owner.speed * Time.deltaTime);
        }
    }
}
