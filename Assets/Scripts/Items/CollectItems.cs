using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectItems : MonoBehaviour
{

    public GameObject[] items;
    public int numberOfCollItemsInAllScenes = 6;
    string[] itemNames;

    int collectedItems = 0;
    public TMPro.TextMeshProUGUI itemText;
    public GameObject itemsCanvas;
    int itemsNumber;
    int curItemNumber = 0;
    bool showItemsText = false;
    string curItemName;


    void Start()
    {
        itemsNumber = items.Length;
        for (int i = 0; i< items.Length; i++)
        {
            //itemNames[i] = items[i].name;
        }
        GameManager.instance.SetNumberOfAllCollectableItems(numberOfCollItemsInAllScenes);
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
        if (other.CompareTag("CollectableItem") && curItemName!=other.name)
        {
            curItemName = other.name;
            curItemNumber++;
            showItemsText = true;
            itemText.text = curItemNumber.ToString() + " / " + itemsNumber.ToString();
            other.gameObject.SetActive(false);

            GameManager.instance.IncreaseNumberOfCollectedItems();
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
