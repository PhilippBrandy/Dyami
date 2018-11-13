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
        if (_owner.isTriggered)
        {
            _owner.guardStateMachine.ChangeState(TriggeredState.Instance);
        }
    }
}
