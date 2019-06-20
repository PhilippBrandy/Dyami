using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    [SerializeField] private LayerMask isNotWall;
    float runSpeed = 120;
    public float maxSpeed = 200;
    public float minSpeed = 120;
    public float crawlSpeed = 60;

    float horizontalMove = 0f;

    public GameObject crawlingRig;
    public bool jump = false;
    bool canMove = true;


    void Update()
    {
        //Debug.Log(canMove);

        bool crawling = crawlingRig.activeSelf;

        if (hInput.GetButton("Sprint") && !crawling)
        {
            runSpeed = maxSpeed;
        }
        else if (crawling)
        {
            runSpeed = crawlSpeed;
        }
        else
        {
            runSpeed = minSpeed;
        }

        horizontalMove = hInput.GetAxis("Horizontal") * runSpeed;

        if (hInput.GetButtonDown("Jump") && !crawling)
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
            jump = false;
        }
    }

    public void DisablePlayerMovement()
    {
        canMove = false;
    }

    public void EnablePlayerMovement()
    {
        canMove = true;
    }
}
