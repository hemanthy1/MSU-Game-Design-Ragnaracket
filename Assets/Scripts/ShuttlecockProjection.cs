using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuttlecockProjection : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.1f;
    //[SerializeField]
    //private float tolerance = 1f;

    private Vector3 targetPoint = new Vector3();
    private Vector3 directAxis = new Vector3();
    private Vector3 normal = new Vector3();
    private Vector3 midpoint = new Vector3();

    private float horizontalCoefficient;
    private float verticalCoefficient;
    //private float speed;

    public bool moving = true;

    bool speedNeg = false;

    public void Initialize(float spd, Vector3 target, Vector3 direct, Vector3 n, Vector3 mid, float hori, float verti)
    {
        //speed = spd;
        targetPoint = target;
        directAxis = direct;
        normal = n;
        midpoint = mid;
        horizontalCoefficient = hori;
        verticalCoefficient = verti;

        if (transform.position.x > targetPoint.x)
            speedNeg = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moving)
        {
            //for (int i = 0; i < speedMultiplier; i++)
            //{
                Vector3 positionUpdate = GetMovementX() + GetMovementY() + GetMovementZ();
                transform.position += positionUpdate;
                transform.rotation = Quaternion.LookRotation(positionUpdate);
            //}
        }
        CheckPosition();
    }

    private Vector3 GetMovementX()
    {
        return speed * directAxis;
    }

    private Vector3 GetMovementY()
    {
        return 2 * verticalCoefficient * (transform.position.x - midpoint.x) * speed * normal;
    }

    private Vector3 GetMovementZ()
    {
        //return new Vector3(0, 0, 0);
        return 2 * horizontalCoefficient * (transform.position.x - midpoint.x) * speed * new Vector3(0, 0, 1);
    }

    private void CheckPosition()
    {
        if (speedNeg)
        {
            if (transform.position.x <= targetPoint.x)
            {
                //Debug.Log("SpeedNegative: " + speedNeg + "; " + transform.position.x + " <= " + targetPoint.x);
                moving = false;
            }
        }
        else if (transform.position.x >= targetPoint.x)
        {
            //Debug.Log("SpeedNegative: " + speedNeg + "; " + transform.position.x + " >= " + targetPoint.x);
            moving = false;
        }
    }
}
