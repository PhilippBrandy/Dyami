using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public bool playOnTrigger;

    public AudioSource source;
    public double fadeTime;

    //fade-in or out?
    private bool increase;

    //Volume
    public double maxVol;
    private double currentVol;
    private double startVol;

    public enum FadeState { sleeping, fading, active};
    private FadeState currentState = FadeState.sleeping;

    //Volumesteps for fade
    private double volumeStep;

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
                source.volume = (float)currentVol;
                break;
            case FadeState.active:
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Berechne notwendige Schrittgröße
            if (currentState == FadeState.sleeping)
            {
                if (playOnTrigger)
                {
                    source.Play();
                }
                currentVol = source.volume;
                if (currentVol < maxVol)
                {
                    increase = true;
                }
                else
                {
                    increase = false;
                }
                volumeStep = (double)((maxVol - currentVol) / fadeTime);
                changeState(FadeState.fading);
            }
            Debug.Log("audio trigger");
            Debug.Log("Volume Step: " + volumeStep);

        }
        
    }

    void changeState(FadeState nextState)
    {
        currentState = nextState;
    }
}



