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
    private ClawJoint claw;
    private Transform catapults;

    [SerializeField]
    private float maxSpeed = 0.01f;
    [SerializeField]
    private float accel = 0.05f;
    [SerializeField]
    private float tolerance = 0.5f;
    [SerializeField]
    private float regenerationTime = 3f;

    private float regenTimer = 0f;
    private bool disabled = false;

    // Start is called before the first frame update
    void Start()
    {
        catapults = transform.parent.Find("Catapults");
        targets.Add(0, transform.parent.Find("EnemyHome"));
        targets.Add(1, transform.parent.Find("TargetIndicator"));
        targets.Add(3, transform.parent.Find("DisabledHold"));
        target = targets[1].GetComponent<TargetIndicator>();
        claw = transform.Find("Model").Find("arm").Find("BaseJoint").Find("SegmentJoint").Find("ClawJoint").GetComponent<ClawJoint>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (disabled)
        {
            Regenerate();
        }
        else
        {
            if (currentTarget == 0 && target.IsActive())
            {
                ToggleTarget();
            }
            else if (currentTarget == 1 && !target.IsActive())
            {
                ToggleTarget();
            }

            Vector3 dist = (targets[currentTarget].position - transform.position);
            dist.x = 0;
            if (dist.magnitude < tolerance)
            {
                //Debug.Log("Linear");
                UpdateMovementLinear();
            }
            else
            {
                UpdateMovementWithAccel();
            }
        }
    }

    private void UpdateMovementLinear()
    {
        Vector3 move = Vector3.MoveTowards(transform.position, targets[currentTarget].position, maxSpeed);
        move.x = transform.position.x;
        transform.position = move;
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

    public void Disable()
    {
        disabled = true;
        currentTarget = 3;
        GetComponent<Collider>().enabled = false;
    }

    private void Regenerate()
    {
        regenTimer += Time.deltaTime;
        UpdateMovementLinear();
        if (regenTimer > regenerationTime)
        {
            regenTimer = 0f;
            disabled = false;
            currentTarget = 0;
            GetComponent<Collider>().enabled = true;
            GetComponent<EnemyStamina>().RegenStamina();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Shuttlecock")
        {
            other.GetComponent<ShuttlecockMotion>().NextTarget();
            target.Hide();
            claw.StartSwing();
            VolleyManager.instance.AddVolley();
            GetComponent<AudioSource>().Play();

            UpdateCatapults();
        }
    }

    private void UpdateCatapults()
    {
        foreach (Transform child in catapults)
        {
            child.GetComponent<Catapult>().SpeedUp();
        }
    }
}
