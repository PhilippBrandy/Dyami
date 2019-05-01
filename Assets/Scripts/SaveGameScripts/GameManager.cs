using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Vector3 startPosition;
    public static GameManager instance = null;

    //public KeyCode left { get; set; }
    //public KeyCode right { get; set; }
    //public KeyCode jump { get; set; }

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

        //jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jump", "Space"));
        //left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("left", "A"));
        //right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("right", "D"));
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
