using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemPortal : MonoBehaviour
{
    public GameObject eyes1;
    public GameObject keystone1;
    public GameObject eyes2;
    public GameObject keystone2;
    public GameObject eyes3;
    public GameObject keystone3;
    public GameObject teleporter;

    public Sprite gateOpen;
    private string combination = "";
    private bool active1 = false;
    private bool active2 = false;
    private bool active3 = false;
    // Start is called before the first frame update
    void Start()
    {
        eyes1.SetActive(false);
        eyes2.SetActive(false);
        eyes3.SetActive(false);
        teleporter.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (keystone1.activeInHierarchy == true && active1 == false)
        {
            eyes1.SetActive(true);
            combination += "1";
        }
        if (keystone2.activeInHierarchy == true && active2 == false)
        {
            eyes2.SetActive(true);
            combination += "2";
        }
        if (keystone3.activeInHierarchy == true && active3 == false)
        {
            eyes3.SetActive(true);
            combination += "3";
        }
        if (combination.Length == 3)
        {
            if (combination == "123")
            {
                teleporter.SetActive(true);
            }
            else
            {
                combination = "";
                eyes1.SetActive(false);
                eyes2.SetActive(false);
                eyes3.SetActive(false);
                active1 = false;
                active2 = false;
                active3 = false;
            }
        }
    }
}
