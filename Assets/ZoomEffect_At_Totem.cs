using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomEffect_At_Totem : MonoBehaviour {
    // Use this for initialization
    public double zoomStart;
    public double maxHeightDistance;
    public Transform totem;
    public double maxSize;
    public double startSize;

    private Camera camera;
    private double sizeDifference;

	void Start ()
    {
        totem = FindObjectOfType<Totem_Controller>().transform;
        camera = GetComponent<Camera>();
        startSize = camera.orthographicSize;
        sizeDifference = maxSize - startSize;
    }
	
	// Update is called once per frame
	void Update ()
    {
        double distance = Mathf.Abs(totem.position.x - transform.position.x);

        if (distance < zoomStart && Mathf.Abs(transform.position.x - totem.position.x) < maxHeightDistance)
        {
            double percentOfWay = distance / zoomStart * 100;
            double sizeAdd = sizeDifference * percentOfWay / 100;
            camera.orthographicSize = (float)(startSize + sizeAdd);
        }
	}
}
