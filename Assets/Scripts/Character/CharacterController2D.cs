
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 0.4f;                          // Amount of force added when the player jumps.
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private LayerMask m_WhatIsSlippy;                          // A mask determining what is slippy to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings

    public Animator animBody;
    public bool crawling;
    public GameObject normalRig;
    public GameObject crawlingRig;

    //Walksound_Sources
    public AudioSource grassWalk;
    public AudioSource stoneWalk;
    public AudioSource woodWalk;
    public AudioSource landingAndWalljump;

    //int jumpHash = Animator.StringToHash("Jump");


    const float k_GroundedRadius = 0.8f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded = true;            // Whether or not the player is grounded.
    const float k_CeilingRadius = .4f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;
    private bool canMove = true;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        //transform.position = GameManager.instance.GetPlayerPos();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }

    private void stopWalkSounds()
    {
        stoneWalk.Stop();
        grassWalk.Stop();
        woodWalk.Stop();
    }

    private void FixedUpdate()
    {
       
        //animBody.SetBool("Crawl", crawling);
        //normalRig.SetActive(!crawling);
        //crawlingRig.SetActive(crawling);

        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded) {
                    if(m_Rigidbody2D.velocity.y <= 0)
                    landingAndWalljump.Play();
					OnLandEvent.Invoke();
				}
                //if player moves
                if (Input.GetAxis("Horizontal") != 0)
                {
                    //and is not crawling
                    if (!GetComponent<PlayerMovement>().crawlingRig.activeSelf)
                    {
                        string name = colliders[i].gameObject.name;
                        //if player is on stone
                        if (name.Contains("cave") || name.Contains("stone") || name.Contains("Stone"))
                        {
                            //Debug.Log("I make stone sounds");
                            if (!stoneWalk.isPlaying)
                            {
                                stoneWalk.Play();
                                if (woodWalk.isPlaying)
                                {
                                    woodWalk.Stop();
                                }
                                if (grassWalk.isPlaying)
                                {
                                    grassWalk.Stop();
                                }
                            }
                        }
                        //if player is on grass
                        else if (name.Contains("Grass") || name.Contains("Moos") || name.Contains("Moss") || name.Contains("ShootDownPlatform"))
                        {
                            //Debug.Log("I make grass sounds");
                            if (!grassWalk.isPlaying)
                            {
                                grassWalk.Play();
                                if (woodWalk.isPlaying)
                                {
                                    woodWalk.Stop();
                                }
                                if (stoneWalk.isPlaying)
                                {
                                    stoneWalk.Stop();
                                }
                            }
                        }
                        else
                        {
                            //Debug.Log("I make wood sounds");
                            if (!woodWalk.isPlaying)
                            {
                                woodWalk.Play();
                                if (stoneWalk.isPlaying)
                                {
                                    stoneWalk.Stop();
                                }
                                if (grassWalk.isPlaying)
                                {
                                    grassWalk.Stop();
                                }
                            }
                        }
                    }
                }
                else
                {
                    stopWalkSounds();
                }
            }
        }
        if (colliders.Length == 0)
        {
            stopWalkSounds();
        }
    }


    public void Move(float move, bool jump)
    {


        if (m_Grounded) canMove = true;
        //only control the player if grounded or airControl is turned on
        Vector2 slippyCheck1 = new Vector2(m_GroundCheck.position.x - 0.5f, m_GroundCheck.position.y - 1);
        Vector2 slippyCheck2 = new Vector2(1f, -10f);
        Collider2D[] colliders = Physics2D.OverlapBoxAll(slippyCheck1, slippyCheck2, 0f, m_WhatIsSlippy);
        //Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius - 0.4f, m_WhatIsSlippy);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                canMove = false;
            }
        }
        float direction = 0f;
        if (m_FacingRight) direction = 10f;
        else direction = -10f;

        float speed = Input.GetAxis("Vertical");
        animBody.SetFloat("Speed", move);
        //Debug.Log(canMove);
        if (canMove)
        {
            if (m_Grounded || m_Rigidbody2D.velocity.y == 0)
            {
                //Debug.Log("Ich bin hier");
                // Move the character by finding the target velocity
                Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
                // And then smoothing it out and applying it to the character
                m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
            }
            else if ((move >= 0 && 7 > m_Rigidbody2D.velocity.x) || (move <= 0 && m_Rigidbody2D.velocity.x > -7))
            {

                m_Rigidbody2D.AddForce(new Vector2(move * 500f, 0));

            }
        }

        else if (jump)
        {
            m_Rigidbody2D.AddForce(new Vector2(move * direction, 0));
        }


        // If the player should jump...
        if ((m_Grounded || m_Rigidbody2D.velocity.y == 0) && jump && !crawling)
        {
            // Add a vertical force to the player.
            m_Grounded = false;
            //animBody.SetTrigger(jumpHash);
            //Springen auf slippy bzw. nicht slippy
            if (canMove) m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_JumpForce);
            else m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_JumpForce * 0.75f);
        }

        // If the input is moving the player right and the player is facing left...
        if (move > 0 && !m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (move < 0 && m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;
    }

    public bool getGrounded()
    {
        return m_Grounded;
    }

    //private bool hitswall()
    //{

    //    bool hit = physics2d.linecast(m_rigidbody2d.transform.position, m_groundcheck.position, 1 << layermask.nametolayer("ground"));
    //    if (hit)
    //    {
    //        debug.log(hit);
    //    }

    //    return hit;
    //}
}