using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuttlecockRotation : MonoBehaviour
{
    public bool reverseSpin = false;
    public float rotationSpeed = 0.25f;
    private Vector3 rotationVector;

    void Start()
    {
        rotationVector = new Vector3(0, 0, rotationSpeed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.parent.GetComponent<ShuttlecockMotion>().IsMoving())
        {
            if (reverseSpin)
                transform.Rotate(-rotationVector);
            else
                transform.Rotate(rotationVector);
        }
    }
}
