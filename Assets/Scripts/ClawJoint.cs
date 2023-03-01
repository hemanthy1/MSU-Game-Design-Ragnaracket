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

    private float t = 0f;
    public float swingTime = 0.5f;

    void Start()
    {
        baseAngle = transform.rotation;
        targetAngle = Quaternion.Euler(transform.rotation.eulerAngles - new Vector3(0, 0, swingAngle));
        //Debug.Log("Base: " + baseAngle.eulerAngles + "\nTarget: " + targetAngle.eulerAngles);
        //Debug.Log(Mathf.Lerp(transform.rotation.eulerAngles.z, targetAngle, swingSpeed));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (mode == 0)
        {
            transform.rotation = Quaternion.Lerp(baseAngle, targetAngle, t);
            //Debug.Log("Lerp " + transform.rotation.eulerAngles.z + " and " + targetAngle + ":\n" + Mathf.Lerp(transform.rotation.eulerAngles.z, targetAngle, swingSpeed));
            t += Time.deltaTime / swingTime;
            if (t >= 1)
            {
                //Debug.Log("swing forward finished");
                mode = 1;
                t = 0;
            }
        }
        else if (mode == 1)
        {
            transform.rotation = Quaternion.Slerp(targetAngle, baseAngle, t);
            t += Time.deltaTime / swingTime;
            if (t >= 1)
            {
                //Debug.Log("swing backward finished");
                mode = -1;
                t = 0;
            }
        }
    }

    public void StartSwing()
    {
        transform.rotation = baseAngle;
        mode = 0;
        t = 0;
    }
}
