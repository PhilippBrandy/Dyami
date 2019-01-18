using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Totem_Controller : MonoBehaviour {
    public VideoPlayer videoPlayer;
    public GameObject passiveTotem;
    public GameObject activeTotem;
    private bool isActive;
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
            videoPlayer.Play();
            passiveTotem.SetActive(false);
            isActive = true;
        }
    }
}
