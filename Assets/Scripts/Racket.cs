using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : MonoBehaviour
{
    private AudioSource hitSound;
    // Start is called before the first frame update
    void Start()
    {
        hitSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Shuttlecock")
        {
            hitSound.Play();
            other.transform.parent.GetComponent<ShuttlecockMotion>().NextTarget();
            VolleyManager.instance.AddVolley();
        }
    }
}
