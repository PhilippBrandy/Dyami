using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateRelated;

public class TriggeredState : State<Guard>
{
    private static TriggeredState _instance;
    private Transform target;
    private GameObject stopPoint;

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

        //spawn empty at entry-position
        target = _owner.player;
        stopPoint = new GameObject("stopPoint");
        stopPoint.tag = "stop";
        stopPoint.transform.position = target.transform.position;

        Vector3 yDif = new Vector3(0, _owner.transform.position.y - stopPoint.transform.position.y);
        stopPoint.transform.Translate(yDif);
        

        //flip to look into right direction
        if (stopPoint.transform.position.x < _owner.transform.position.x)
        {
            _owner.transform.eulerAngles = new Vector3(0, -180, 0);
            direction = Vector3.left;
        }
        else
        {
            _owner.transform.eulerAngles = new Vector3(0, 0, 0);
            direction = Vector3.right;
        }
    }

    public override void ExitState(Guard _owner)
    {
        Debug.Log("Exiting Triggered State");
    }

    public override void UpdateState(Guard _owner)
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(_owner.groundDetection.position, Vector2.down, _owner.detDistance);
        RaycastHit2D attackInfo = Physics2D.Raycast(_owner.attackDetection.position, Vector2.right, _owner.attackDistance);

        if (groundInfo.collider == true)
        {
            if (currentSpeed < _owner.Speed)
            {
                currentSpeed += _owner.Beschleunigung * Time.deltaTime;
            }
            if(attackInfo.collider == true)
            {
                if (attackInfo.collider.CompareTag("Player"))
                {
                    _owner.isAttacking = true;
                }
                float speed = currentSpeed;
            }
            
            _owner.transform.position += direction * currentSpeed * Time.deltaTime;
        }
        else
        {
            changeState(_owner);
        }
    }

    private void changeState(Guard _owner)
    {
        currentSpeed = 0f;
        _owner.isTriggered = false;
        UnityEngine.Object.Destroy(stopPoint.gameObject);
        _owner.guardStateMachine.ChangeState(IdleState.Instance);
    }
}
