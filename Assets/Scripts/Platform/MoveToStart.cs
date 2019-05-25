using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToStart : MonoBehaviour
{
    bool movesRight = true;
    bool movesLeft = true;
    Vector3 startPosition;
    public float speed = 1.0f;
    bool hasPFReachedEndPos = false;
    float magnitude = 0.3f;

    private void Start()
    {
        startPosition = this.transform.position;
    }
    
    private void Update()
    {
        if (!movesRight && !movesLeft)
        {
            StartCoroutine(MovePFToStartPosition());
        }
    }

    public void isPFMovingRight(bool flag)
    {
        movesRight = flag;
    }

    public void isPFMovingLeft(bool flag)
    {
        movesLeft = flag;
    }

    IEnumerator MovePFToStartPosition()
    {
        while (Vector3.Distance(this.transform.position, startPosition) > magnitude && !hasPFReachedEndPos)
        {
            float step = speed * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(this.transform.position, startPosition, step);
            yield return 0;
        }
        this.transform.position = startPosition;
        hasPFReachedEndPos = true;
    }
}
