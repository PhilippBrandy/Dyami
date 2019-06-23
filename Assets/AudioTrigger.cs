using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public bool playOnTrigger;

    public AudioSource source;
    public float fadeTime;

    //fade-in or out?
    private bool increase;

    //Volume
    public float maxVol;
    private float currentVol;
    private float startVol;

    public enum FadeState { sleeping, fading, active};
    private FadeState currentState = FadeState.sleeping;

    //Volumesteps for fade
    private float volumeStep;

    // Start is called before the first frame update
    void Start()
    {
        startVol = source.volume;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case FadeState.sleeping:
                break;
            case FadeState.fading:
                if (increase)
                {
                    if (currentVol < maxVol)
                    {
                        currentVol += volumeStep * Time.deltaTime;
                    }
                    else
                    {
                        changeState(FadeState.active);
                    }
                }
                else
                {
                    if (currentVol > maxVol)
                    {
                        currentVol += volumeStep * Time.deltaTime;
                    }
                    else
                    {
                        changeState(FadeState.active);
                    }
                }
                source.volume = currentVol;
                break;
            case FadeState.active:
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (playOnTrigger)
        {
            source.Play();
        }
        //Berechne notwendige Schrittgröße
        if (currentState == FadeState.sleeping)
        {
            currentVol = source.volume;
            if (currentVol < maxVol)
            {
                increase = true;
            }
            else
            {
                increase = false;
            }
            volumeStep = (float)(maxVol - currentVol / fadeTime);
            changeState(FadeState.fading);
        }
            Debug.Log("audio trigger");
    }

    void changeState(FadeState nextState)
    {
        currentState = nextState;
    }
}



