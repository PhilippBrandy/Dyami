using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {

    private Transform cameraTranform;
    private Transform[] layers;
    private float viewZone = 10;
    private int leftIndex;
    private int rightIndex;

    public float backgroundSize;

    private void Start()
    {
        cameraTranform = Camera.main.transform;
        layers = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            layers[i] = transform.GetChild(i);
        }
        leftIndex = 0;
        rightIndex = layers.Length - 1;
    }


    private void Update()
    {
        if(cameraTranform.position.x < (layers[leftIndex].transform.position.x + viewZone))
        {
            ScrollLeft();
        }

        if (cameraTranform.position.x > (layers[rightIndex].transform.position.x + viewZone))
        {
            ScrollRight();
        }
    }

    private void ScrollLeft()
    {
        int lastRight = rightIndex;
        layers[rightIndex].position = new Vector3(1 * layers[leftIndex].position.x - backgroundSize, layers[leftIndex].position.y, 0);//Vector3.right * (layers[leftIndex].position.x - backgroundSize);
        leftIndex = rightIndex;

        rightIndex--;
        if (rightIndex < 0)
        {
            rightIndex = layers.Length - 1;
        }  
    }


    private void ScrollRight()
    {
        int lastLeft = leftIndex;
        layers[leftIndex].position = new Vector3(1 * layers[rightIndex].position.x + backgroundSize, layers[rightIndex].position.y, 0);//Vector3.right * (layers[rightIndex].position.x + backgroundSize);
        rightIndex = leftIndex;
        rightIndex++;
        if (leftIndex == layers.Length)
        {
            leftIndex = 0;
        }
    }

}
