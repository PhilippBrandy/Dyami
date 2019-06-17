using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Totem_Controller : MonoBehaviour {
    public VideoPlayer videoPlayer;
    public GameObject passiveTotem;
    public GameObject activeTotem;
    public GameObject activeTotem2;
    private bool isActive;
    private IEnumerator coroutine;
    public float TimertoActivateActiveTotem2;

    // Use this for initialization
    void Start () {
        videoPlayer = activeTotem.GetComponent<VideoPlayer>();
        isActive = false;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isActive == false)
        {
            collision.GetComponent<Killable>().spawnpoint = gameObject.transform;
            activeTotem.SetActive(true);
            coroutine = WaitAndWatch(TimertoActivateActiveTotem2);
            StartCoroutine(coroutine);
            videoPlayer.Play();
          passiveTotem.SetActive(false);
            isActive = true;
        }
    }
    private IEnumerator WaitAndWatch(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        activeTotem2.SetActive(true);
        activeTotem2.GetComponent<EmissionPulse>().enabled = true;

    }
}
