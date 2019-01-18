using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialSP : MonoBehaviour
{
    public double fadeDuration;

    public Camera playerCam;
    public double maxSize;
    private double baseSize;
    private double baseLoudness;
    public double playVolume;

    public int maxDistance;
    public int triggerHeigth;
    public int minDistance;
    public AudioSource backgroundMusic;
    public AudioSource overwrite;
    public AudioSource roar;

    public CameraState currentState { get; private set; }

    // states
    private SleepState sleeping;
    private ZoomingOutState zoomingOut;
    private ActiveState active;
    private ZoomingInState zoomingIn;

    public enum SSPState { sleeping, zoomingout, active, zoomingin };
    private SSPState currentSSPState = SSPState.sleeping;

    //Step-values
    public float sizeStep;
    public float volumeStep;
    public float volumeGainStep;

    // Start is called before the first frame update
    private void Awake()
    {
        //Generate States
        sleeping = new SleepState();
        zoomingOut = new ZoomingOutState();
        zoomingIn = new ZoomingInState();
        active = new ActiveState();

        //Get Base-Values
        currentState = sleeping;
        baseSize = playerCam.orthographicSize;
        baseLoudness = backgroundMusic.volume;

        //Calculate Steps
        sizeStep = (float)((baseSize - maxSize) / fadeDuration);
        volumeStep = (float)(baseLoudness / fadeDuration);
        volumeGainStep = (float)(playVolume / fadeDuration);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == sleeping)
        {
            if (Mathf.Abs(playerCam.transform.position.x - gameObject.transform.position.x) <= maxDistance && Mathf.Abs(playerCam.transform.position.y - gameObject.transform.position.y) <= triggerHeigth)
                {
                    changeState(SSPState.zoomingout);
                }
        }
        else if (currentState == zoomingOut)
        {
            if (baseSize < maxSize)
            {
                if (playerCam.orthographicSize >= maxSize)
                {
                    changeState(SSPState.active);
                }
            }
            else
            {
                if (playerCam.orthographicSize <= maxSize)
                {
                    changeState(SSPState.active);
                }
            }
        }
        else if (currentState == active)
        {
            if (Mathf.Abs(playerCam.transform.position.x - gameObject.transform.position.x) > maxDistance || Mathf.Abs(playerCam.transform.position.y - gameObject.transform.position.y) > triggerHeigth)
            {
                changeState(SSPState.zoomingin);
            }
        }
        else if (currentState == zoomingIn)
        {
            if (baseSize < maxSize)
            {
                if (playerCam.orthographicSize <= baseSize)
                {
                    changeState(SSPState.sleeping);
                }
            }
            else
            {
                if (playerCam.orthographicSize <= baseSize)
                {
                    changeState(SSPState.sleeping);
                }
            }
        }
        currentState.UpdateState(this);
    }


    void changeState(SSPState nextState)
    {
        currentSSPState = nextState;
        currentState.ExitState(this);
        switch (nextState)
        {
            case SSPState.sleeping:
                currentState = sleeping;
                break;
            case SSPState.zoomingout:
                currentState = zoomingOut;
                break;
            case SSPState.active:
                currentState = active;
                break;
            case SSPState.zoomingin:
                currentState = zoomingIn;
                break;
        }
        currentState.EnterState(this);
    }
}



public abstract class CameraState
{
    public abstract void EnterState(SpecialSP owner);
    public abstract void ExitState(SpecialSP owner);
    public abstract void UpdateState(SpecialSP owner);
}

internal class SleepState : CameraState
{
    public override void EnterState(SpecialSP owner)
    {

    }

    public override void ExitState(SpecialSP owner)
    {

    }

    public override void UpdateState(SpecialSP owner)
    {

    }
}

internal class ZoomingOutState : CameraState
{
    public override void EnterState(SpecialSP owner)
    {
        owner.roar.Play();
    }

    public override void ExitState(SpecialSP owner)
    {

    }

    public override void UpdateState(SpecialSP owner)
    {
        owner.playerCam.orthographicSize -= owner.sizeStep * Time.deltaTime;
        owner.backgroundMusic.volume -= owner.volumeStep * Time.deltaTime;
        owner.overwrite.volume += owner.volumeGainStep * Time.deltaTime;
    }
}

internal class ActiveState : CameraState
{
    public override void EnterState(SpecialSP owner)
    {

    }

    public override void ExitState(SpecialSP owner)
    {

    }

    public override void UpdateState(SpecialSP owner)
    {

    }
}


internal class ZoomingInState : CameraState
{
    public override void EnterState(SpecialSP owner)
    {

    }

    public override void ExitState(SpecialSP owner)
    {

    }

    public override void UpdateState(SpecialSP owner)
    {
        owner.playerCam.orthographicSize += owner.sizeStep * Time.deltaTime;
        owner.backgroundMusic.volume += owner.volumeStep * Time.deltaTime;
        owner.overwrite.volume -= owner.volumeGainStep * Time.deltaTime;
    }
}




