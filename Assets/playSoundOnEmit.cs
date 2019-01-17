using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playSoundOnEmit : MonoBehaviour
{
    public Camera playerCam;
    private ParticleSystem emitter;
    private AudioSource soundFX;
    public int hearDistance;

    // Start is called before the first frame update
    void Start()
    {
        soundFX = gameObject.GetComponent<AudioSource>();
        emitter = gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
            if (emitter.particleCount >= 1)
            {
                soundFX.Play();
            }
    }
}
