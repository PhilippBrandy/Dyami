using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectItems : MonoBehaviour
{

    public GameObject[] items;
    int collectedItems = 0;
    public TMPro.TextMeshProUGUI itemText;
    public GameObject itemsCanvas;
    int itemsNumber;
    int curItemNumber = 0;
    bool showItemsText = false;


    void Start()
    {
        itemsNumber = items.Length;
    }

    void Update()
    {
        if (showItemsText)
        {
            itemsCanvas.SetActive(true);
        }

        if (curItemNumber == itemsNumber)
        {
            // all items collected
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("CollectableItem"))
        {
            curItemNumber++;
            showItemsText = true;
            itemText.text = curItemNumber.ToString() + " / " + itemsNumber.ToString();
            other.gameObject.SetActive(false);
        }
    }


    public int GetItemsNumber()
    {
        return items.Length;
    }

    public int GetItemsCollected()
    {
        return collectedItems;
    }

    public void SetItemsCollectedNumber(int newNumber)
    {
        curItemNumber = newNumber;
    }

    public void SetItemsCanvasInvisible()
    {
        showItemsText = false;
        itemsCanvas.SetActive(false);
    }
        
}
