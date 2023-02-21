using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawJoint : MonoBehaviour
{
    public float swingAngle = 30f;
    public float swingSpeed = 0.5f;
    public float tolerance = 5f;

    //private fields for calculating animation
    private int mode = -1; // -1: idle; 0: swing forward; 1: rewind to original position;
    private Quaternion baseAngle;
    private Quaternion targetAngle;

    void Start()
    {
        baseAngle = transform.rotation;
        targetAngle = Quaternion.Euler(transform.rotation.eulerAngles - new Vector3(0, 0, swingAngle));
        Debug.Log("Base: " + baseAngle.eulerAngles + "\nTarget: " + targetAngle.eulerAngles);
        //Debug.Log(Mathf.Lerp(transform.rotation.eulerAngles.z, targetAngle, swingSpeed));
    }

    // Update is called once per frame
    void Update()
    {
        if (mode == 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetAngle, swingSpeed);
            //Debug.Log("Lerp " + transform.rotation.eulerAngles.z + " and " + targetAngle + ":\n" + Mathf.Lerp(transform.rotation.eulerAngles.z, targetAngle, swingSpeed));
            if (Mathf.Abs(transform.rotation.eulerAngles.z - targetAngle.eulerAngles.z) <= tolerance)
            {
                Debug.Log("swing forward finished");
                mode = 1;
            }
            else
            {
                Debug.Log("angle: " + transform.rotation.eulerAngles + "\ntarget: " + targetAngle);
            }
        }
        else if (mode == 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, baseAngle, swingSpeed);
            if (Mathf.Abs(transform.rotation.eulerAngles.z - baseAngle.eulerAngles.z) <= tolerance)
            {
                Debug.Log("swing backward finished");
                mode = -1;
            }
        }
    }

    public void StartSwing()
    {
        transform.rotation = baseAngle;
        mode = 0;
    }
}
