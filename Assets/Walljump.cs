using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Walljump : MonoBehaviour
{
    public LayerMask m_WhatIsWall;
    public float playerWidth = 1.5f;
    public float jumpStrength = 30;
    public float slideSpeedDivisor = 6;
    private float baseGravity;
    private Rigidbody2D rb;

    RaycastHit2D wallRight;
    RaycastHit2D wallLeft;


    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        baseGravity = rb.gravityScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Is the player grounded?
        bool grounded = this.GetComponent<CharacterController2D>().getGrounded();

        //Find walls to the left/right of the player
        wallRight = Physics2D.Raycast(transform.position, Vector2.right, playerWidth, m_WhatIsWall);
        wallLeft = Physics2D.Raycast(transform.position, Vector2.left, playerWidth, m_WhatIsWall);

        //When the player is next to a wall and not on the ground
        if ((wallRight.collider != null || wallLeft.collider != null) && !grounded)
        {
            //remove control and reduce gravity
            this.GetComponent<PlayerMovement>().enabled = false;

            //reduce the gravity if the player is moving against a wall and falls
            if (rb.velocity.y < -1 && hugsWall())
            {
                rb.gravityScale = baseGravity / slideSpeedDivisor;
            }
            else rb.gravityScale = baseGravity;

            // When the player is moving against a wall to their right
            if (wallRight.collider != null)
            {
                //They can jump
                if (hInput.GetButtonDown("Jump"))
                {
                    rb.velocity = new Vector2(-jumpStrength, jumpStrength * 1.125f);
                    Invoke("reset", 0.3f);
                    rb.gravityScale = baseGravity;
                }
                if (hatesWall())
                {
                    rb.gravityScale = baseGravity;
                    rb.AddForce(new Vector2(-jumpStrength * 10, 0));
                }
            }

            //same for walls to their left
            else if (wallLeft.collider != null)
            {
                if (hInput.GetButtonDown("Jump"))
                {
                    rb.velocity = new Vector2(jumpStrength, jumpStrength * 1.125f);
                    Invoke("reset", 0.3f);
                    rb.gravityScale = baseGravity;
                }
                if (hatesWall())
                {
                    Debug.Log("I hate walls");
                    rb.gravityScale = baseGravity;
                    rb.AddForce(new Vector2(jumpStrength * 10, 0));
                }
            }
        }
        else if (grounded)
        {
            reset();
            rb.gravityScale = baseGravity;
        }
        if (this.GetComponent<ShootArrow>().playerTeleports())
        {
            rb.gravityScale = baseGravity;
        }
    }

    //Enables the PlayerMovement Script again
    private void reset()
    {
        if (!hugsWall())
        {
            this.GetComponent<PlayerMovement>().enabled = true;
        }
    }

    //Returns true if player is moving against a wall next to them
    private bool hugsWall()
    {
        return (wallRight.collider != null && hInput.GetAxis("Horizontal") > 0 || wallLeft.collider != null && hInput.GetAxis("Horizontal") < 0);
    }

    //Returns true if player is moving away from a wall next to them
    private bool hatesWall()
    {
        return (wallRight.collider != null && hInput.GetAxis("Horizontal") < 0 || wallLeft.collider != null && hInput.GetAxis("Horizontal") > 0);
    }
}
