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
    private Rigidbody2D rb = null;
    private bool canTeleport = true;
    private GameObject projectile = null;
    bool hit = false;
    [SerializeField] private LayerMask getsStuckIn;
    [SerializeField] private Transform shotPoint;


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
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - rp.transform.position;
        float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        rp.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        //Shoot Arrow
        if (Input.GetMouseButtonDown(0))
        {
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
            
            diff.y *= 1.25f;
            diff.x /= 1.25f;

            //Move arrow
            projectile.transform.position = transform.position + Camera.main.transform.forward * 20;
            rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = diff * strength;

        }

        //Teleport
        if (Input.GetMouseButtonDown(1) && projectile != null && canTeleport)
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

        }

        //Rotate arrow depending on velocity
        if (projectile != null && !(rb.velocity.x == 0 || rb.velocity.x == 0))
        {
            Vector2 v = rb.velocity;
            float angle_arrow = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg + 90;
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
                }
            }
        }
        
    }
}
