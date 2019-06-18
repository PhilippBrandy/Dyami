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
  
    public GameObject[] glowingAssets5;


    public string featherTrigger;
  
    // Start is called before the first frame update
    void Start()
    {
        feather.SetActive(true);
        characterWeapon.learnedTeleporting = false;
        featherpower = true;
        textObject.SetActive(false);

}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            characterWeapon.learnedTeleporting = true;
             if (featherpower == true)
        {
                Debug.Log("feather");
            featherAnim.SetTrigger(featherTrigger);

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
}
