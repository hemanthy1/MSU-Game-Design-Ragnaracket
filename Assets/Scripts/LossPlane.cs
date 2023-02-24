using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LossPlane : MonoBehaviour
{
    private Transform respawnPoint;
    private TrebuchetAnimation trebuchet;

    public int shuttleMisses = 0;

    void Start()
    {
        respawnPoint = transform.Find("RespawnPoint");
        trebuchet = transform.Find("trebuchet_anim").GetComponent<TrebuchetAnimation>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Shuttlecock")
        {
            other.transform.position = respawnPoint.position;
            other.GetComponent<ShuttlecockMotion>().NextTarget();
            other.GetComponent<ShuttlecockMotion>().ResetSpeed();
            other.GetComponent<ShuttlecockMotion>().SetMoving(false);
            trebuchet.AnimationStart();
            VolleyManager.instance.ResetVolley();
            shuttleMisses += 1;
        }
    }
}
