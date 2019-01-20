using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArrow : MonoBehaviour
{
    public float offset = -90;
    public float strength = 10;
    public GameObject arrow;
    public GameObject player;
    public GameObject rp; //rotation point
    public GameObject crosshair;
    private Rigidbody2D rb = null;
    private bool canTeleport = true;
    public bool learnedTeleporting = false; 
    private GameObject projectile = null;
    bool hit = false;
    [SerializeField] private LayerMask getsStuckIn;
    public Animator anim;

    int shootHash = Animator.StringToHash("Shoot");

    //forceemitter while in air after teleport
    public bool theForce = false;

    private void FixedUpdate()
    {

        //makes the player able to teleport again when they are on the ground
        if (!canTeleport)
        {
            if (player.GetComponent<CharacterController2D>().getGrounded())
            {
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
        crosshair.transform.position = mousePos;
        rp.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);




        //Shoot Arrow
        if (Input.GetMouseButtonDown(0))
        {
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

        }

        //Teleport
        if (Input.GetMouseButtonDown(1) && projectile != null && canTeleport && learnedTeleporting)
        {
            Rigidbody2D playerRig = player.GetComponent<Rigidbody2D>();
            player.transform.position = new Vector2(projectile.transform.position.x, projectile.transform.position.y + 0.5f);
            
            if (hit)
            {
                playerRig.velocity = Vector3.zero;
            } else {
                playerRig.velocity = rb.velocity*0.7f;
            }
            canTeleport = false;
            //start force-code
            theForce = true;
            //end force-code

            Destroy(projectile);
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
        theRotation.y = 180.0f * i;
        player.transform.localRotation = theRotation;
    }
}
