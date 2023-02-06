using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : MonoBehaviour
{
    private AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Shuttlecock")
        {
            sound.Play();
            other.transform.parent.GetComponent<ShuttlecockMotion>().NextTarget();
        }
    }
}
