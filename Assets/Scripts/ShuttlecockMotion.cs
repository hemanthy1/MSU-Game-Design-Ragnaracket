using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuttlecockMotion : MonoBehaviour
{
    // Fields used to keep track of target
    [Tooltip("List of target planes")]
    public List<VolleyPlane> targets = new List<VolleyPlane>();
    private int nextTarget = 0;
    private Vector3 targetPoint = new Vector3();
    private Vector3 directAxis = new Vector3();
    private Vector3 normal = new Vector3();
    private Vector3 midpoint = new Vector3();

    // Fields used to calculate movement
    public bool moving = true;
    private float speed = 0.01f;
    private float horizontalCoefficient = 1f;
    private float verticalCoefficient = 1f;

    private const float coefficientMin = .25f;
    private const float coefficientMax = .75f;
    private const float rotationSpeed = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        if (targets.Count <= 0)
            moving = false;
        else
        {
            targetPoint = transform.position;
            NextTarget();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            Vector3 positionUpdate = GetMovementX() + GetMovementY() + GetMovementZ();
            transform.position += positionUpdate;
            transform.rotation = Quaternion.LookRotation(positionUpdate);
        }
    }

    private void CalculateParameters()
    {
        horizontalCoefficient = Random.Range(-coefficientMax, coefficientMax);
        verticalCoefficient = Random.Range(coefficientMin, coefficientMax);
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

    public void NextTarget()
    {
        nextTarget++;
        if (nextTarget >= targets.Count)
            nextTarget = 0;

        Vector3 newTargetPoint = targets[nextTarget].GenerateTarget();
        directAxis = newTargetPoint - transform.position;
        Vector3.Normalize(directAxis);
        normal = Vector3.Cross(directAxis, new Vector3(0, 0, 1)).normalized;
        midpoint = Vector3.Lerp(newTargetPoint, transform.position, 0.5f);
        targetPoint = newTargetPoint;

        CalculateParameters();
    }

    public void UpdateSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
