using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class EnemyDefender : MonoBehaviour
{
    private TargetIndicator target;
    private Dictionary<int, Transform> targets = new Dictionary<int, Transform>();
    private int currentTarget = 0;

    private Vector3 currentVelocity = new Vector3(0, 0, 0);
    private float currentSpeed = 0f;

    [SerializeField]
    private float maxSpeed = 0.01f;
    [SerializeField]
    private float accel = 0.05f;
    [SerializeField]
    private float tolerance = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        targets.Add(0, transform.parent.Find("EnemyHome"));
        targets.Add(1, transform.parent.Find("TargetIndicator"));
        target = targets[1].GetComponent<TargetIndicator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentTarget == 0 && target.IsActive())
        {
            ToggleTarget();
        }
        else if (currentTarget == 1 && !target.IsActive())
        {
            ToggleTarget();
        }
        else if ((targets[currentTarget].position - transform.position).magnitude < tolerance)
        {
            ResetVelocity();
        }

        UpdateMovementWithAccel();
        //UpdateMovementLinear();
    }

    private void UpdateMovementLinear()
    {
        //Vector3 distance = (targets[currentTarget].position - transform.position).normalized;
        //distance.x = 0;
        //transform.position += distance * currentSpeed;
        transform.position = Vector3.MoveTowards(transform.position, targets[currentTarget].position, maxSpeed);
    }

    private void UpdateMovementWithAccel()
    {
        Vector3 distance = (targets[currentTarget].position - transform.position).normalized;
        distance.x = 0;
        currentVelocity += distance * accel;
        if (currentVelocity.magnitude > maxSpeed)
        {
            currentVelocity.Normalize();
            currentVelocity *= maxSpeed;
        }
        transform.position += currentVelocity;
    }

    private void ResetVelocity()
    {
        currentVelocity = new Vector3(0, 0, 0);
        currentSpeed = 0;
    }
    
    private void ToggleTarget()
    {
        currentTarget = (currentTarget == 0 ? 1 : 0);
        currentSpeed = maxSpeed;
    }
}
