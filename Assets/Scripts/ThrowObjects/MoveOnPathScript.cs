using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnPathScript : MonoBehaviour
{
    public EditorPathScript PathToFollow;
    public int curWayPoint = 0;
    public float speed;
    // the distance between the object and the cur waypoint
    public float reachDistance = 1.0f;
    public float rotationSpeed = 5.0f;
    public string pathName;
    Vector3 lastPosition;
    Vector3 curPosition;

    void Start()
    {
        // for random paths
        //PathToFollow = GameObject.Find(pathName).GetComponent<EditorPathScript>();
        lastPosition = transform.position;
    }

    void Update()
    {
        float distance = Vector3.Distance(PathToFollow.pathObjects[curWayPoint].position, transform.position);
        transform.position = Vector3.Lerp(transform.position, PathToFollow.pathObjects[curWayPoint].position, Time.deltaTime * speed);
        // or MoveTowards
        //transform.position = Vector3.MoveTowards(transform.position, PathToFollow.pathObjects[curWayPoint].position, Time.deltaTime * speed);

        Vector3 moveDirection = PathToFollow.pathObjects[curWayPoint].position - transform.position;
        if (moveDirection != Vector3.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

 
        //Vector3 relativePos = PathToFollow.pathObjects[curWayPoint].position - transform.position;
        //Quaternion LookAtRotation = Quaternion.LookRotation(relativePos);
        //Quaternion LookAtRotationOnly_Y = Quaternion.Euler(transform.rotation.eulerAngles.x, LookAtRotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        //transform.rotation = LookAtRotationOnly_Y;
        //Quaternion LookAtRotationOnly_X = Quaternion.Euler(LookAtRotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        //transform.rotation = LookAtRotationOnly_X;

        //var rotation = Quaternion.LookRotation(PathToFollow.pathObjects[curWayPoint].position - transform.position);
        //transform.rotation.x = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

        if (distance <= reachDistance)
        {
            curWayPoint++;
        }

        if (curWayPoint >= PathToFollow.pathObjects.Count)
        {
            // or destroy the object here
            curWayPoint = 0;
        }
    }
}
