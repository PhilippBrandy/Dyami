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
<<<<<<< HEAD
=======
    private bool resetControl;

    public Animator animHead;
    public Animator animBody;
    public Animator animArms;

    int hangingHash, climbHash, airHash, wallHash;
    bool inAir, atWall = false;
>>>>>>> Animations

    RaycastHit2D wallRight, wallLeft, edgeRight, edgeLeft, nearGround;


    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        baseGravity = rb.gravityScale;

        hangingHash = Animator.StringToHash("Hanging");
        climbHash = Animator.StringToHash("Climb");
        airHash = Animator.StringToHash("inAir");
        wallHash = Animator.StringToHash("atWall");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Is the player grounded?
        grounded = this.GetComponent<CharacterController2D>().getGrounded();
        Debug.Log(grounded);

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
        int direction = 1;
        if (wallRight.collider != null) direction = -1;
        else direction = 1;
        inAir = !grounded;
        animArms.SetBool(wallHash, false);

        if (((wallRight.collider != null && edgeRight.collider == null) || (wallLeft.collider != null && edgeLeft.collider == null)) && rb.velocity.y <= 0 && nearGround.collider == null)
        {
            Debug.Log("I'm at an edge");
            wasOnWall = true;
            //remove control and gravity
            this.GetComponent<PlayerMovement>().enabled = false;
            rb.gravityScale = 0.0f;
            rb.velocity = Vector3.zero;
            atWall = true;
            animBody.SetTrigger(hangingHash);
            animHead.SetTrigger(hangingHash);
            animArms.SetBool(wallHash, true);

            if (wallLeft.collider != null || wallRight.collider != null) 
            {
                //They can jump
                if (hInput.GetButtonDown("Jump"))
                {
                    rb.velocity = new Vector2(jumpStrength * direction, jumpStrength * 1.125f);
                    reset();
                    rb.gravityScale = baseGravity;
                    atWall = false;
                }
                //Climb
                else if (hugsWall(direction))
                {
                    Debug.Log("I would climb");
                    animArms.SetTrigger(climbHash);
                    atWall = false;

                    rb.velocity = new Vector2(0, jumpStrength * 1.125f);
                    animArms.SetTrigger(climbHash);
                    animBody.SetTrigger(climbHash);
                    animHead.SetTrigger(climbHash);
                    reset();
                    //rb.gravityScale = baseGravity;
                }
                //or Fall down
                else if (hatesWall(direction))
                {
                    rb.gravityScale = baseGravity;
                    rb.AddForce(new Vector2(jumpStrength * 40 * direction, 0));
                    reset();
                    atWall = false;
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
            if (rb.velocity.y < -0.5 && hugsWall(direction))
            {
                rb.gravityScale = baseGravity / slideSpeedDivisor;
                if (rb.velocity.y < -5) rb.velocity = new Vector3(rb.velocity.x, -5);
            }
            else rb.gravityScale = baseGravity;

            //They can jump away
            if (hInput.GetButtonDown("Jump"))
            {
                rb.velocity = new Vector2(jumpStrength * direction, jumpStrength * 1.125f);
                Invoke("reset", 0.6f);
                atWall = false;
                rb.gravityScale = baseGravity;
            }
            //Slide
            else if (hugsWall(direction))
            {
                Debug.Log("I slide");
                animArms.SetTrigger(climbHash);
                atWall = true;
                reset();
                //rb.gravityScale = baseGravity;
            }
            //Or let themselves fall
            else if (hatesWall(direction))
            {
                rb.gravityScale = baseGravity;
                rb.AddForce(new Vector2(jumpStrength * 10 * direction, 0));
                reset();
            }
        }
        else if (wasOnWall) {
<<<<<<< HEAD
            reset();
=======
            Invoke("reset", 0.8f);
>>>>>>> Animations
            wasOnWall = false;
            rb.gravityScale = baseGravity;
        }
        if (this.GetComponent<ShootArrow>().playerTeleports())
        {
            rb.gravityScale = baseGravity;
        }
        animBody.SetBool(airHash, inAir);
        animBody.SetBool(wallHash, atWall);
        if (resetControl)
        {
            atWall = false;
            resetControls();
        }
    }

    //Enables the PlayerMovement Script again
    private void reset()
    {
        resetControl = true;
    }

    private void resetControls()
    {
        if ((!hugsWall() && !grounded) || grounded)
        {
            this.GetComponent<PlayerMovement>().enabled = true;
        }
        resetControl = false;
    }

    //Returns true if player is moving against a wall next to them
    private bool hugsWall()
    {
        return (wallRight.collider != null && hInput.GetAxis("Horizontal") > 0 || wallLeft.collider != null && hInput.GetAxis("Horizontal") < 0);
    }

    private bool hugsWall(int direction)
    {
        if (direction == -1) return (wallRight.collider != null && hInput.GetAxis("Horizontal") > 0);
        else return (wallLeft.collider != null && hInput.GetAxis("Horizontal") < 0);
    }

    //Returns true if player is moving away from a wall next to them
    private bool hatesWall(int direction)
    {
        if (direction == -1) return (wallRight.collider != null && hInput.GetAxis("Horizontal") < 0);
        else return (wallLeft.collider != null && hInput.GetAxis("Horizontal") > 0);
    }
}
