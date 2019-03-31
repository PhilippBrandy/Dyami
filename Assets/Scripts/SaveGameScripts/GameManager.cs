using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Vector3 startPosition;
    public static GameManager instance = null;


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


    public void SetPlayerPos(Vector3 position)
    {
        startPosition = position;
    }

    public Vector3 GetPlayerPos()
    {
        Debug.Log(startPosition);
        if (startPosition == null)
        {
            startPosition.x = -228.2f;
            startPosition.y = 110.5f;
            startPosition.z = 0.0f;
        }
        return startPosition;
    }
}
