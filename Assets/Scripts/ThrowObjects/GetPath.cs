using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPath : MonoBehaviour
{
    // for setting up random paths the object follows
    // drag it on throwable object holder
    public GameObject[] allPaths;

    void Start()
    {
        int num = Random.Range(0, allPaths.Length);
        transform.position = allPaths[num].transform.position;
        MoveOnPathScript yourPath = GetComponent<MoveOnPathScript>();
        yourPath.pathName = allPaths[num].name;
    }
}
