using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walljump : MonoBehaviour
{
    public LayerMask m_WhatIsWall;
    public float playerWidth = 1.5f;
    public float jumpStrength = 25;
    public float slideSpeedDivisor = 6;
    private float baseGravity;
    private Rigidbody2D rb;
    private bool grounded;

    private bool wasOnWall;

    RaycastHit2D wallRight, wallLeft, edgeRight, edgeLeft, nearGround;


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
        grounded = this.GetComponent<CharacterController2D>().getGrounded();

        //Find walls to the left/right of the player
        wallRight = Physics2D.Raycast(transform.position, Vector2.right, playerWidth, m_WhatIsWall);
        wallLeft = Physics2D.Raycast(transform.position, Vector2.left, playerWidth, m_WhatIsWall);

        edgeRight = Physics2D.Raycast(transform.position + new Vector3(0, 1, 0), Vector2.right, playerWidth, m_WhatIsWall);
        edgeLeft = Physics2D.Raycast(transform.position + new Vector3(0, 1, 0), Vector2.left, playerWidth, m_WhatIsWall);

        nearGround = Physics2D.Raycast(transform.position + new Vector3(0, -2, 0), Vector2.down, 3, m_WhatIsWall);
        Debug.Log(nearGround.collider);

        //-------------------------------------
        //Edge climb
        //-------------------------------------

        //When the player is falling, next to an edge (wall next to them and air above) and not near the ground
        if (((wallRight.collider != null && edgeRight.collider == null) || (wallLeft.collider != null && edgeLeft.collider == null)) && rb.velocity.y <= 0 && nearGround.collider == null)
        {
            Debug.Log("I'm at an edge");
            wasOnWall = true;
            //remove control and gravity
            this.GetComponent<PlayerMovement>().enabled = false;
            rb.gravityScale = 0.0f;
            rb.velocity = Vector3.zero;

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
                //Climb
                else if (hugsWall())
                {
                    Debug.Log("I would climb");
                    //Invoke("reset", 0.3f);
                    //rb.gravityScale = baseGravity;
                }
                //or Fall down
                else if (hatesWall())
                {
                    rb.gravityScale = baseGravity;
                    rb.AddForce(new Vector2(-jumpStrength * 40, 0));
                    reset();
                }
            }

            //same for walls to their left
            else if (wallLeft.collider != null)
            {
                //They can jump
                if (hInput.GetButtonDown("Jump"))
                {
                    rb.velocity = new Vector2(jumpStrength, jumpStrength * 1.125f);
                    Invoke("reset", 0.3f);
                    rb.gravityScale = baseGravity;
                }
                //Climb
                else if (hugsWall())
                {
                    Debug.Log("I would climb");
                    //Invoke("reset", 0.3f);
                    //rb.gravityScale = baseGravity;
                }
                //or Fall down
                else if (hatesWall())
                {
                    rb.gravityScale = baseGravity;
                    rb.AddForce(new Vector2(jumpStrength * 40, 0));
                    reset();
                }
            }
        }
        //-------------------------------------
        //Wall Jump
        //-------------------------------------

        //When the player is next to a wall and not on the ground and on near an edge (not full wall)
        else if ((wallRight.collider != null || wallLeft.collider != null) && nearGround.collider == null)
        {
            Debug.Log("I'm at a wall");
            wasOnWall = true;
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
                //They can jump away
                if (hInput.GetButtonDown("Jump"))
                {
                    rb.velocity = new Vector2(-jumpStrength, jumpStrength * 1.125f);
                    Invoke("reset", 0.3f);
                    rb.gravityScale = baseGravity;
                }
                //Or let themselves fall
                else if (hatesWall())
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
                else if (hatesWall())
                {
                    Debug.Log("I hate walls");
                    rb.gravityScale = baseGravity;
                    rb.AddForce(new Vector2(jumpStrength * 10, 0));
                }
            }
        }
        else if (wasOnWall) {
            reset();
            wasOnWall = false;
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
        if ((!hugsWall() && !grounded) || grounded)
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
