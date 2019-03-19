using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddleManager : MonoBehaviour
{
    public Keystone key1;
    public Keystone key2;
    public Keystone key3;
    public string password;
    public string collectedPassword;
    public GameObject toActivate;
    public GameObject toDeactivate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (collectedPassword.Length == 3)
        {
            if (collectedPassword == password)
            {
                toActivate.SetActive(true);
                toDeactivate.SetActive(false);
            }
            else
            {
                deactivateAll();
            }
        }
    }


    private void deactivateAll()
    {
        key1.deactivate();
        key2.deactivate();
        key3.deactivate();
        password = "";
    }
}
