using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArrow : MonoBehaviour
{
    public float offset = 0;
    public float strength = 20;
    public GameObject arrow;
    private Rigidbody2D rb = null;
    private GameObject projectile = null;
    bool hit = false;
    float depth = 0.5f;
    [SerializeField] private LayerMask getsStuckIn;
    [SerializeField] private Transform shotPoint;
    //[SerializeField] private Transform hitCheck;


    private void FixedUpdate()
    {

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

        if (projectile != null && !(rb.velocity.x == 0 || rb.velocity.x == 0))
        {
            Vector2 v = rb.velocity;
            float angle_arrow = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg + 90;
            projectile.transform.rotation = Quaternion.AngleAxis(angle_arrow, Vector3.forward);
        }

        if (rb != null)
        {
            if (!hit && rb.GetComponent<Collider2D>() != null)
            {
                if (rb.GetComponent<Collider2D>().IsTouchingLayers(getsStuckIn))
                {
                    hit = true;
                    rb.isKinematic = true;
                    rb.velocity = Vector3.zero;
                    Debug.Log("test123");
                }
            }
        }
        
    }
}
