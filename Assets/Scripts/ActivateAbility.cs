using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateAbility : MonoBehaviour
{
    public ShootArrow characterWeapon;
   public GameObject feather;
    public Animator featherAnim;
    private bool featherpower;
    public GameObject textObject;
    public GameObject playerPointLight;
    public GameObject textObject1;
    public GameObject textObject2;
    public GameObject[] glowingAssets5;
    public bool playOnce;
    public AudioSource eagleScream;

    public string featherTrigger;
  
    // Start is called before the first frame update
    void Start()
    {
        feather.SetActive(true);
        characterWeapon.learnedTeleporting = false;
        featherpower = true;
        textObject.SetActive(false);
        textObject1.SetActive(false);
        textObject2.SetActive(false);
        playOnce = true;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.ActivateTeleporting();

            characterWeapon.learnedTeleporting = true;
             if (featherpower == true && playOnce)
        {Invoke("showText1", 7);
            Invoke("showText2", 14);
                Debug.Log("feather");
            featherAnim.SetTrigger(featherTrigger);
                playOnce = false;
            }


        }
       
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        
        playerPointLight.SetActive(true);
        featherpower = false;
        textObject.SetActive(true);
        feather.SetActive(false);

        for(int i=0; i < glowingAssets5.Length; i++)
        {
            glowingAssets5[i].SetActive(true);
        }

        Invoke("removeText", 20);

        // Destroy(feather);
    }

    private void removeText()
    {
        textObject.SetActive(false);
    }

    //egale text
    private void removeText1()
    {
        textObject1.SetActive(false);
    }
    private void removeText2()
    {
        textObject2.SetActive(false);
    }
    private void showText1()
    {
        eagleScream.Play();
        textObject1.SetActive(true);
        Invoke("removeText1", 7);
    }
    private void showText2()
    {
        eagleScream.Play();

        Invoke("removeText2", 10);
        textObject2.SetActive(true);
    }
}
