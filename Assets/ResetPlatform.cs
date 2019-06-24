using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlatform : MonoBehaviour
{
    public GameObject platform;
    private Transform plTrans;
    private SpriteRenderer plRend;
    private FalldownPlatform plFallPlat;
    private GameObject clone;

    public Killable player;
    private bool respawned;

    // Start is called before the first frame update
    void Start()
    {
        CopyCurrent();
        respawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.health <= 0 && !respawned)
        {
            activateNewAndDeleteOld();
        }
        if (respawned && player.health > 0)
        {
            respawned = false;
        }
    }

    void CopyCurrent()
    {
        clone = Instantiate(platform, gameObject.transform, false);
        clone.SetActive(false);
    }

    void activateNewAndDeleteOld()
    {
        platform.SetActive(false);
        platform = clone;
        platform.SetActive(true);
        CopyCurrent();
        respawned = true;
    }

}
