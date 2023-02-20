using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LossPlane : MonoBehaviour
{
    private Transform respawnPoint;

    void Start()
    {
        respawnPoint = transform.Find("RespawnPoint");
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Shuttlecock")
        {
            other.transform.position = respawnPoint.position;
            other.GetComponent<ShuttlecockMotion>().NextTarget();
            other.GetComponent<ShuttlecockMotion>().ResetSpeed();
        }
        VolleyManager.instance.ResetVolley();
    }
}
