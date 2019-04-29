using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.U2D;


public class testScript : MonoBehaviour
{

    public SpriteShapeController spriteShapeController;
    public int index;
    
  
   
    private Spline spline;
    private int lastSpritePointCount;

    void Awake()
    {
        Debug.Log("awake");

        spline = spriteShapeController.spline;
    }
    private void Update()
    {
        Debug.Log("update");
        spline.SetSpriteIndex(1, 1);
        Invoke("changeIndex1", 1f);
    }

     void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("trigger enter");

            spline.SetSpriteIndex(1, 1);
            Invoke("changeIndex", 1f);
        }

    }

    public void changeIndex()
    {
        Debug.Log("change index");

        spline.SetSpriteIndex(1, 1);
        Invoke("changeIndex1", 1f);
    }
    public void changeIndex1()
    {
        spline.SetSpriteIndex(1, 0);
        Invoke("changeIndex", 1f);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        
        

    }

}
