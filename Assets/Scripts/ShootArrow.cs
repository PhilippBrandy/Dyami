using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArrow : MonoBehaviour
{

    public float offset = -90;
    public float strength = 10;
    public GameObject arrow;
    public GameObject player;
    public GameObject eagle;
    public GameObject rp; //rotation point
    private Rigidbody2D rb = null;
    private bool canTeleport = false;
    private bool hasShot = true;
    public GameObject teleportIndicator;
    public bool learnedTeleporting = false;
    private GameObject projectile = null;
    bool hit = false;
    [SerializeField] private LayerMask getsStuckIn;
    [SerializeField] private LayerMask bouncesOf;
    public Animator animHead;
    public Animator animBody;
    public Animator animArms;
    public Transform bowAimAt;
    public Transform headLookAt;
    public GameObject crawlRig;
    //Is true when player is currently teleporting
    private bool isTeleporting = false;
    //Is true when player has to wait to shoot again
    private bool shootDelay = false;
    //Arrow without glow effect
    public Sprite normal_arrow;
    //Defines how many arrows can exist at once
    public int maxArrows = 5;
    //Defines if a player shot an arrow after learning how to teleport
    private bool firstTeleportShot = false;

    private float eagleScale;
    private Vector3 arrowVelocity;
    private float speed;

    private LinkedList<GameObject> arrows = new LinkedList<GameObject>();

    bool facesRight = true;
    int shootHash = Animator.StringToHash("Shoot");
    int facingHash = Animator.StringToHash("facingRight");

    //forceemitter while in air after teleport
    public bool theForce = false;
    private bool canShoot = true;

    //audiofiles for telport and shooting
    public AudioSource shootArrow;
    public AudioSource audioSource;
    public AudioClip[] characterSoundFx;
    public AudioClip[] teleportfx;
    int soundsIndex = 0;

    //Shockwave
    public float shockWaveLength;


    private void Start()
    {
        eagleScale = eagle.transform.localScale.y;
    }

    private void FixedUpdate()
    {
        if (!canTeleport)
        {
            //makes the player able to teleport again when they are on the ground
            if (player.GetComponent<CharacterController2D>().getGrounded() && firstTeleportShot)
            {
                //TODO: Abprüfen ob er nochmal geschossen hat nachdem er gelandet ist
                canTeleport = true;

                //start force-code
                theForce = false;
                //end force-code
            }

            //Disable indicator that the player can telepor with their next shot
            else if (teleportIndicator != null)
            {
                teleportIndicator.SetActive(false);
            }
        }

        if (canShoot)
        {
            //Rotate Bow towards direction of mouse
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 diff = mousePos - rp.transform.position;
            if (mousePos.x > rp.transform.position.x) playerFaces(0);
            else playerFaces(1);
            float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

            if (!facesRight)
            {
                if (mousePos.y < rp.transform.position.y) angle += 180;
                else angle -= 180;
                rp.transform.rotation = Quaternion.AngleAxis(angle + 135, Vector3.forward);
                headLookAt.localRotation = Quaternion.AngleAxis(angle / 2, Vector3.back);
                //angle += 90;
                bowAimAt.localRotation = Quaternion.AngleAxis(angle, Vector3.back);
            }
            else
            {
                rp.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                headLookAt.localRotation = Quaternion.AngleAxis(angle / 2, Vector3.forward);
                bowAimAt.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }


            //Shoot Arrow
            bool crawls = crawlRig.activeSelf;

            if (Input.GetMouseButtonDown(0) && !isTeleporting && !shootDelay && !crawls)
            {
                //plays the shootArrow sound fx
                // audioSource.PlayOneShot(characterSoundFx[0]);
                shootArrow.Play();
                if (canTeleport) hasShot = true;
                if (learnedTeleporting) firstTeleportShot = true;
                animArms.SetTrigger(shootHash);

                if (arrows.Count >= maxArrows)
                {
                    Destroy(arrows.Last.Value);
                    arrows.RemoveLast();
                }
                hit = false;
                if (projectile != null)
                {
                    projectile.GetComponent<SpriteRenderer>().sprite = normal_arrow;
                    projectile.GetComponentInChildren<Light>().enabled = false;
                }
                projectile = Instantiate(arrow);
                teleportIndicator.SetActive(true);
                arrows.AddFirst(projectile);
                if (!canTeleport || !learnedTeleporting)
                {
                    teleportIndicator.SetActive(false);
                    projectile.GetComponent<SpriteRenderer>().sprite = normal_arrow;
                    projectile.GetComponentInChildren<Light>().enabled = false;
                }
                shootDelay = true;
                Invoke("rechargeBow", 1);


                //make arrow strength independent of distance:
                float div = 1f;
                if (Mathf.Abs(diff.x) > Mathf.Abs(diff.y))
                {
                    div = Mathf.Abs(diff.x);
                }
                else
                {
                    div = Mathf.Abs(diff.y);
                }

                diff.x = diff.x / div;
                diff.y = diff.y / div;

                diff.y *= 1.10f;
                diff.x /= 1.05f;

                //Move arrow
                projectile.transform.position = transform.position;
                rb = projectile.GetComponent<Rigidbody2D>();
                rb.velocity = diff * strength;
                //Invoke("resetArms", 1);
            }
        }

        //Teleport
        if (playerTeleports())
        {
            isTeleporting = true;
            //player.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            Rigidbody2D arrowRB = projectile.GetComponent<Rigidbody2D>();
            if (arrowRB != null)
            {
                arrowVelocity = arrowRB.velocity;
                arrowRB.velocity = Vector3.zero;
                arrowRB.isKinematic = true;
            }
            player.GetComponent<Killable>().enabled = false;
            player.GetComponent<PlayerMovement>().enabled = false;
            Rigidbody2D playerRB = player.GetComponent<Rigidbody2D>();
            playerRB.velocity = Vector2.zero;
            playerRB.isKinematic = true;
            foreach (BoxCollider2D collider in player.GetComponents<BoxCollider2D>())
            {
                collider.enabled = false;
            }
            SpriteRenderer[] renderers;
            renderers = GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer renderer in renderers)
            {
                renderer.enabled = false;
            }

            // sound randomizer
            soundsIndex = Random.Range(0, teleportfx.Length);
            audioSource.PlayOneShot(teleportfx[soundsIndex]);
            //curSound = teleportSounds[soundsIndex];
            //curSound.Play();

            eagle.SetActive(true);
            Vector3 range = player.transform.position - projectile.transform.position;
            speed = Mathf.Sqrt((Mathf.Pow(range.y, 2)) + (Mathf.Pow(range.x, 2)));
            Invoke("playerTeleported", 0.02f * speed);
        }

        if (isTeleporting)
        {
            Vector3 playerPos = player.transform.position;
            Vector2 movetowards = Vector2.MoveTowards(new Vector2(playerPos.x, playerPos.y), projectile.transform.position, 50 * Time.deltaTime);

            Vector2 eagleDir = projectile.transform.position - eagle.transform.position;
            eagle.transform.right = eagleDir;

            if(!facesRight)
            {
                eagle.transform.localScale = new Vector3(eagleScale, -eagleScale, eagleScale);
            }
            else
            {
                eagle.transform.localScale = new Vector3(eagleScale, eagleScale, eagleScale);
            }

            playerPos = new Vector3(movetowards.x, movetowards.y, player.transform.position.z);
            player.transform.position = playerPos;
        }

        //Rotate arrow depending on velocity
        if (projectile != null && rb != null && !(rb.velocity.x == 0 || rb.velocity.x == 0))
        {
            Vector2 v = rb.velocity;
            float angle_arrow = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.AngleAxis(angle_arrow, Vector3.forward);
        }

        //Stop arrow on impact
        if (rb != null)
        {
            if (!hit && rb != null)
            {
                if (rb.IsTouchingLayers(getsStuckIn))
                {
                    hit = true;
                    rb.isKinematic = true;
                    rb.velocity = Vector3.zero;
                    Collider2D[] iHitThis = new Collider2D[1];
                    ContactFilter2D filter = new ContactFilter2D();
                    filter.SetLayerMask(getsStuckIn);
                    rb.OverlapCollider(filter, iHitThis);
                    if (iHitThis[0] != null)
                    {

                        projectile.GetComponent<Transform>().SetParent(iHitThis[0].GetComponent<Transform>());

                        /*  ParentConstraint Test
                        UnityEngine.Animations.ParentConstraint parentconstraint = projectile.GetComponent<UnityEngine.Animations.ParentConstraint>();

                        //dummy source
                        UnityEngine.Animations.ConstraintSource source = new UnityEngine.Animations.ConstraintSource();
                        //add parent transform as source
                        source.sourceTransform = iHitThis[0].GetComponent<Transform>();

                        //Child moves like parent:
                        parentconstraint.weight = 1f;
                        source.weight = 1f;

                        //Activate constraint
                        parentconstraint.AddSource(source);
                        parentconstraint.constraintActive = true;
                        */
                        Vector3 parentScale = iHitThis[0].GetComponent<Transform>().lossyScale;
                        projectile.GetComponent<Transform>().localScale = new Vector3(2 / parentScale.x, 2 / parentScale.y, parentScale.z);
                        Destroy(projectile.GetComponent<Rigidbody2D>());
                        Collider2D[] colliders = projectile.GetComponents<Collider2D>();
                        for (int i = 0; i < colliders.Length; i++)
                        {
                            Destroy(colliders[i]);
                        }
                    }                 
                }

                else if (rb.IsTouchingLayers(bouncesOf))
                {
                    Collider2D[] iHitThis = new Collider2D[1];
                    ContactFilter2D filter = new ContactFilter2D();
                    filter.SetLayerMask(bouncesOf);
                    rb.OverlapCollider(filter, iHitThis);
                    if (iHitThis[0] != null)
                    {
                        Debug.Log("hi");
                        rb.GetComponent<CircleCollider2D>().enabled = true;
                        Invoke("stopBounce", 0.1f);
                    }
                }
            }
        }
    }
    private void stopBounce()
    {
        rb.GetComponent<CircleCollider2D>().enabled = false;
    }

    public void playerFaces(int i)
    {
        // Multiply the player's x local scale by -1.
        Quaternion theRotation = player.transform.localRotation;
        theRotation.y = 180f * i;
        player.transform.localRotation = theRotation;
        if (i == 0) facesRight = true;
        else facesRight = false;
        animBody.SetBool(facingHash, facesRight);
    }

    /*private void resetArms()
    {
        AnimArms.SetActive(false);
        NormalArms.SetActive(true);
    }*/

    private void playerTeleported()
    {
        #region
        //Dash-Shockwave
        LayerMask layerMask = LayerMask.GetMask("Default");
        RaycastHit2D shockWaveHit = Physics2D.Raycast(this.projectile.transform.position, this.projectile.transform.right, shockWaveLength, layerMask.value);
        if (shockWaveHit.collider != null)
        {
            DashableObject dashed = shockWaveHit.collider.gameObject.GetComponentInParent<DashableObject>();
            if (dashed != null)
            {
                dashed.triggerAnimation();
            }
            BreakableEnvi destroyed = shockWaveHit.collider.gameObject.GetComponentInParent<BreakableEnvi>();
            if (destroyed != null)
            {
                destroyed.triggerBreaking();
            }
            Keystone keystone = shockWaveHit.collider.gameObject.GetComponentInParent<Keystone>();
            if (keystone != null)
            {
                keystone.activateKey();
            }
        }




        #endregion
        hasShot = false;
        Rigidbody2D playerRig = player.GetComponent<Rigidbody2D>();
        if (hit)
        {
            playerRig.velocity = Vector3.zero;
        }
        else
        {
            playerRig.velocity = arrowVelocity * 0.7f;
        }
        canTeleport = false;
        //start force-code
        theForce = true;
        //end force-code

        Destroy(projectile);
        arrows.Remove(projectile);

        //player.transform.localScale = playerScale;
        //player.transform.position = new Vector3(projectile.transform.position.x, projectile.transform.position.y + 0.5f, 0.0f);
        player.GetComponent<Killable>().enabled = true;
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<Rigidbody2D>().isKinematic = false;
        foreach (BoxCollider2D collider in player.GetComponents<BoxCollider2D>())
        {
            collider.enabled = true;
        }

        SpriteRenderer[] renderers;
        renderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer renderer in renderers)
        {
            renderer.enabled = true;
        }
        isTeleporting = false;
        eagle.SetActive(false);
        teleportIndicator.SetActive(false);

    }

    //Returns true if the player meets the requirements to teleport and rightclicks
    public bool playerTeleports()
    {
        return (Input.GetMouseButtonDown(1) && projectile != null && canTeleport && learnedTeleporting && hasShot && !isTeleporting);
    }

    public bool isPlayerTeleporting()
    {
        return isTeleporting;
    }

    //Makes player able to shoot an arrow again
    private void rechargeBow()
    {
        shootDelay = false;
    }

    public LinkedList<GameObject> getArrows()
    {
        return arrows;
    }


    public bool getCanShoot()
    {
        return canShoot;
    }

    public void setCanShoot(bool shoot)
    {
        canShoot = shoot;
    }
}
