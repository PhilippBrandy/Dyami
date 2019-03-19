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
  
    // Start is called before the first frame update
    void Start()
    {
        feather.SetActive(true);
        characterWeapon.learnedTeleporting = false;
        featherpower = true;
        textObject.SetActive(false);

}

// Update is called once per frame
void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            characterWeapon.learnedTeleporting = true;
             if (featherpower == true)
        {
            featherAnim.SetTrigger("Start");

        }

        }
       
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        feather.SetActive(false);
        featherpower = false;
        textObject.SetActive(true);
        Invoke("removeText", 20);

        // Destroy(feather);
    }

    private void removeText()
    {
        textObject.SetActive(false);
    }
}
