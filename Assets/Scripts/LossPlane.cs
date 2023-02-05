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
            other.transform.parent.position = respawnPoint.position;
            other.transform.parent.GetComponent<ShuttlecockMotion>().NextTarget();
        }
    }
}
