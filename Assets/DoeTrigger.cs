using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoeTrigger : MonoBehaviour
{
    public Animator animatorDoe;
    public string triggerName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animatorDoe.SetTrigger(triggerName);
    }
}
