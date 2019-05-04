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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
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

        //if (GameManager.instance != null)
        //{
        //    if (Input.GetKeyDown(GameManager.instance.jump))
        //    {
        //        jump = true;
        //    }
        //    if (Input.GetKeyDown(GameManager.instance.left))
        //    {
        //        jump = false;
        //    }
        //    if (Input.GetKeyDown(GameManager.instance.right))
        //    {
        //        jump = false;
        //    }
        //}
    }

    private void FixedUpdate()
    {
        //Move the character
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
    }
}
