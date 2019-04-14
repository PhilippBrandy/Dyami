using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class KeyBinding : MonoBehaviour
{
    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    public Text left, right, jump;
    private GameObject curKey;
    private Color32 normalColor = new Color32(195, 200, 229, 255);
    private Color32 selectedColor = new Color32(122, 134, 207, 255);


    void Start()
    {
        keys.Add("Left", KeyCode.A);
        keys.Add("Right", KeyCode.D);
        keys.Add("Jump", KeyCode.Space);

        left.text = keys["Left"].ToString();
        right.text = keys["Right"].ToString();
        jump.text = keys["Jump"].ToString();
    }

    void OnGUI()
    {
        if (curKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                keys[curKey.name] = e.keyCode;
                curKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                curKey.GetComponent<Image>().color = normalColor;
                curKey = null;
            }
        }
    }

    public void ChangeKey(GameObject clicked)
    {
        if (curKey != null)
        {
            curKey.GetComponent<Image>().color = normalColor;
        }
        curKey = clicked;
        curKey.GetComponent<Image>().color = selectedColor;
    }
}
