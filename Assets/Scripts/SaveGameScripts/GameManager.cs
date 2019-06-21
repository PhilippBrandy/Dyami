using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Vector3 startPosition;
    public static GameManager instance = null;
    public GameObject playerStartposition;
    bool isGameLoaded = false;

    int numberOfAllCollectableItems = 0;
    int numberOfCollectedItems = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);

        }
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        playerStartposition = GameObject.Find("PlayerStartPosition");
    }

    public void SetPlayerPos(Vector3 position)
    {
        startPosition = position;
    }

    public Vector3 GetPlayerPos()
    {
        if (startPosition == null)
        {
            startPosition.x = playerStartposition.transform.position.x;
            startPosition.y = playerStartposition.transform.position.y;
            startPosition.z = 0.0f;
        }
        return startPosition;
    }

   public void SetIsGameLoaded(bool flag)
    {
        isGameLoaded = flag;
    }

    public bool IsGameLoaded()
    {
        return isGameLoaded;
    }

    public void SetNumberOfAllCollectableItems(int number)
    {
        numberOfAllCollectableItems = number;
    }

    public int GetNumberOfAllCollectableItems()
    {
        return numberOfAllCollectableItems;
    }

    public int GetNumberOfCollectedItems()
    {
        return numberOfCollectedItems;
    }

    public void IncreaseNumberOfCollectedItems()
    {
        numberOfCollectedItems++;
    }
}
