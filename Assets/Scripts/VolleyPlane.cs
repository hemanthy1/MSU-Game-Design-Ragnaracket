using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class that describes the planes that the shuttlecock volleys between
 * Written by Adam Cohen
 */

public class VolleyPlane : MonoBehaviour
{

    private float halfWidth;
    private float halfHeight;
    private TargetIndicator targetIndicator;
    [SerializeField]
    private bool autoVolley = false;

    // Start is called before the first frame update
    void Start()
    {
        halfWidth = GetComponent<BoxCollider>().size.z / 2f;
        halfHeight = GetComponent<BoxCollider>().size.y / 2f;
        targetIndicator = transform.Find("TargetIndicator").GetComponent<TargetIndicator>();
    }

    // Controls trigger collisions
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Shuttlecock")
        {
            targetIndicator.Hide();
            if (autoVolley)
            {
                other.transform.parent.GetComponent<ShuttlecockMotion>().NextTarget();
                targetIndicator.Hide();
            }
        }
    }

    // Generates a location  within the bounds of the plane
    public Vector3 GenerateTarget()
    {
        float targetHorizontal = Random.Range(-halfWidth, halfWidth);
        float targetVertical = Random.Range(-halfHeight, halfHeight);
        Vector3 localPoint = new Vector3(0, targetVertical, targetHorizontal);

        targetIndicator.Show(localPoint);
        return transform.position + localPoint;
    }

    public float GetWidth()
    {
        return halfWidth * 2;
    }

    public float GetHeight()
    {
        return halfHeight * 2;
    }
}
