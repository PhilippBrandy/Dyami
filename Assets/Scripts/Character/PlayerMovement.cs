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

    float horizontalMove = 0f;

    public bool jump = false;
    bool canMove = true;


    void Update()
    {
        //Debug.Log(canMove);


        if (hInput.GetButton("Sprint"))
        {
            runSpeed = maxSpeed;
            horizontalMove = hInput.GetAxis("Horizontal") * runSpeed;
        }
        else
        {
            runSpeed = minSpeed;
            horizontalMove = hInput.GetAxis("Horizontal") * runSpeed;
        }

        if (hInput.GetButtonDown("Jump"))
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
