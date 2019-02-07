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
    private bool canTeleport = true;
    private bool hasShot = true;
    public bool learnedTeleporting = false; 
    private GameObject projectile = null;
    bool hit = false;
    [SerializeField] private LayerMask getsStuckIn;
    public Animator anim;
    public Transform bowAimAt;
    public Transform bowAimAt2;
    public Transform headLookAt;
    public GameObject AnimArms;
    public GameObject NormalArms;
    private bool isTeleporting = false;

    private Vector3 playerScale;
    private Vector3 arrowVelocity;
    private float speed;

    bool facesRight = true;
    int shootHash = Animator.StringToHash("Shoot");

    //forceemitter while in air after teleport
    public bool theForce = false;

    private void Start()
    {
        playerScale = player.transform.localScale;
    }

    private void FixedUpdate()
    {

        //makes the player able to teleport again when they are on the ground
        if (!canTeleport)
        {
            if (player.GetComponent<CharacterController2D>().getGrounded())
            {
                //TODO: Abprüfen ob er nochmal geschossen hat nachdem er gelandet ist
                canTeleport = true;

                //start force-code
                theForce = false;
                //end force-code
            }
        }

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
            rp.transform.rotation = Quaternion.AngleAxis(angle+135, Vector3.forward);
            headLookAt.localRotation = Quaternion.AngleAxis(angle/2, Vector3.back);
            angle += 90;
            bowAimAt.localRotation = Quaternion.AngleAxis(angle, Vector3.back);
            bowAimAt2.localRotation = Quaternion.AngleAxis(angle, Vector3.back);
        } else
        {
            rp.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            headLookAt.localRotation = Quaternion.AngleAxis(angle/2, Vector3.forward);
            bowAimAt.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            angle -= 80;
            bowAimAt2.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
            




        //Shoot Arrow
        if (Input.GetMouseButtonDown(0) && !isTeleporting)
        {
            if (canTeleport) hasShot = true;
            AnimArms.SetActive(true);
            NormalArms.SetActive(false);
            anim.SetTrigger(shootHash);
            
            Destroy(projectile);
            hit = false;
            projectile = Instantiate(arrow);
            
            //make arrow strength independent of distance:
            float div = 1f;
            if (Mathf.Abs(diff.x)>Mathf.Abs(diff.y))
            {
                div = Mathf.Abs(diff.x);
            } else {
                div = Mathf.Abs(diff.y);
            }

            diff.x = diff.x / div;
            diff.y = diff.y / div;
            
            diff.y *= 1.10f;
            diff.x /= 1.05f;

            //Move arrow
            projectile.transform.position = transform.position + transform.forward * 20;
            rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = diff * strength;
            Invoke("resetArms", 1);
        }

        //Teleport
        if (Input.GetMouseButtonDown(1) && projectile != null && canTeleport && learnedTeleporting && hasShot && !isTeleporting)
        {
            isTeleporting = true;
            player.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
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
            playerRB.velocity = Vector3.zero;
            playerRB.isKinematic = true;
            player.GetComponent<BoxCollider2D>().enabled = false;
            eagle.SetActive(true);
            Vector3 range = player.transform.position - projectile.transform.position;
            speed = Mathf.Sqrt((Mathf.Pow(range.y, 2)) + (Mathf.Pow(range.x, 2)));
            Invoke("playerTeleported", 0.02f*speed);
        }

        if (isTeleporting)
        {
            Vector3 playerPos = player.transform.position;
            playerPos = Vector2.MoveTowards(new Vector2(playerPos.x, playerPos.y), projectile.transform.position, 50*Time.deltaTime);
            player.transform.position = playerPos;
        }

        //Rotate arrow depending on velocity
        if (projectile != null && rb != null && !(rb.velocity.x == 0 || rb.velocity.x == 0))
        {
            Vector2 v = rb.velocity;
            float angle_arrow = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg + offset;
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
                    }
                    Destroy(projectile.GetComponent<Rigidbody2D>());
                    Collider2D[] colliders = projectile.GetComponents<Collider2D>();
                    for (int i = 0; i<colliders.Length; i++)
                    {
                        Destroy(colliders[i]);
                    }
                }
            }
        }
    }
    private void playerFaces(int i)
    {
        // Multiply the player's x local scale by -1.
        Quaternion theRotation = player.transform.localRotation;
        theRotation.y = 180f * i;
        player.transform.localRotation = theRotation;
        if (i == 0) facesRight = true;
        else facesRight = false;
    }
    private void resetArms()
    {
        AnimArms.SetActive(false);
        NormalArms.SetActive(true);
    }
    private void playerTeleported()
    {
        hasShot = false;
        Rigidbody2D playerRig = player.GetComponent<Rigidbody2D>();
        player.transform.position = new Vector2(projectile.transform.position.x, projectile.transform.position.y + 0.5f);

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
        player.transform.localScale = playerScale;
        player.GetComponent<Killable>().enabled = true;
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<Rigidbody2D>().isKinematic = false;
        player.GetComponent<BoxCollider2D>().enabled = true;
        isTeleporting = false;
        eagle.SetActive(false);
    }
}
