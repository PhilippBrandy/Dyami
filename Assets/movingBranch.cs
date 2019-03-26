using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingBranch : MonoBehaviour
{
    public Animation move;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    move.Play();
}
}
