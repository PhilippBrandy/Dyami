using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArrow : MonoBehaviour
{
    public float offset = 0;
    public float strength = 20;
    public GameObject arrow;
    public GameObject player;
    private Rigidbody2D rb = null;
    private bool canTeleport = true;
    private GameObject projectile = null;
    bool hit = false;
    [SerializeField] private LayerMask getsStuckIn;
    [SerializeField] private Transform shotPoint;


    private void FixedUpdate()
    {

        //makes the player able to teleport again when they are on the ground
        if (!canTeleport)
        {
            if (player.GetComponent<CharacterController2D>().getGrounded())
            {
                canTeleport = true;
            }
        }


        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg + offset;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (Input.GetMouseButtonDown(0))
        {
            Destroy(projectile);
            hit = false;
            projectile = Instantiate(arrow);

            projectile.transform.position = transform.position + Camera.main.transform.forward * 20;
            rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = diff * strength;

        }

        if (Input.GetMouseButtonDown(1) && projectile != null && canTeleport)
        {
            Rigidbody2D playerRig = player.GetComponent<Rigidbody2D>();
            player.transform.position = new Vector2(projectile.transform.position.x, projectile.transform.position.y + 0.5f);
            
            if (hit)
            {
                playerRig.velocity = Vector3.zero;
            } else {
                float x = rb.velocity.x;
                float y = rb.velocity.y;
                playerRig.AddForce(new Vector2(x*50,y*50));
                Debug.Log(rb.velocity);
            }
            canTeleport = false;

        }

        if (projectile != null && !(rb.velocity.x == 0 || rb.velocity.x == 0))
        {
            Vector2 v = rb.velocity;
            float angle_arrow = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg + 90;
            projectile.transform.rotation = Quaternion.AngleAxis(angle_arrow, Vector3.forward);
        }

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
