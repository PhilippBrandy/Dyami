using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorPathScript : MonoBehaviour
{
    public Color rayColor = Color.white;
    public List<Transform> pathObjects = new List<Transform>();
    Transform[] pathArray;

    void OnDrawGizmos()
    {
        Gizmos.color = rayColor;
        pathArray = GetComponentsInChildren<Transform>();
        pathObjects.Clear();

        foreach (Transform o in pathArray)
        {

            if (o != this.transform)
            {

                pathObjects.Add(o);
            }
        }

        for (int i = 0; i < pathObjects.Count; i++)
        {
            Vector3 position = pathObjects[i].position;
            if (i > 0)
            {
                Vector3 prevois = pathObjects[i - 1].position;
                Gizmos.DrawLine(prevois, position);
                Gizmos.DrawWireSphere(position, 0.3f);
            }
        }
    }
}
