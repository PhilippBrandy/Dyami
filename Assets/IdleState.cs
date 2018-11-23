using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateRelated;

public class IdleState : State<Guard>
{
    private static IdleState _instance;

    private IdleState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static IdleState Instance
    {
        get
        {
            if (_instance == null)
            {
                new IdleState();
            }

            return _instance;
        }
    }

    public override void EnterState(Guard _owner)
    {
        Debug.Log("Entering Idle State");
    }

    public override void ExitState(Guard _owner)
    {
        Debug.Log("Exiting Idle State");
    }

    public override void UpdateState(Guard _owner)
    {
        float distance = _owner.transform.position.x - _owner.player.position.x;
        if (distance < 0)
        {
            distance = distance * -1;
        }
        if (distance <= _owner.triggerDistance)
        {
            _owner.isTriggered = true;
            _owner.guardStateMachine.ChangeState(TriggeredState.Instance);
        }
    }
}
