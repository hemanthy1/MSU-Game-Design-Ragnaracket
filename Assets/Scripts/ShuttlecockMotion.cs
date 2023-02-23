using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuttlecockMotion : MonoBehaviour
{
    // Fields used to keep track of target
    [Tooltip("List of target planes")]
    public List<VolleyPlane> targets = new List<VolleyPlane>();
    [SerializeField]
    private GameObject projectionPrefab;
    private int nextTarget = 0;
    private Vector3 targetPoint = new Vector3();
    private Vector3 directAxis = new Vector3();
    private Vector3 normal = new Vector3();
    private Vector3 midpoint = new Vector3();

    // Fields used to calculate movement
    public bool moving = true;
    public float baseSpeed = 0.01f;
    public float speedGrowth = 0.002f;
    public float speedCap = 0.03f;
    private float speed = 0.01f;
    private float horizontalCoefficient = 1f;
    private float verticalCoefficient = 1f;

    private const float coefficientMin = 0.01f;
    private const float coefficientMax = 0.5f;
    private const float rotationSpeed = 0.25f;

    private float tempSpeedMultiplier = 1f;

    // Start is called before the first frame update
    void Start()
    {
        speed = baseSpeed;
        if (targets.Count <= 0)
            moving = false;
        else
        {
            targetPoint = transform.position;
            NextTarget();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
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
        return speed * tempSpeedMultiplier * directAxis;
    }

    private Vector3 GetMovementY()
    {
        return 2 * verticalCoefficient * (transform.position.x - midpoint.x) * speed * tempSpeedMultiplier * normal;
    }

    private Vector3 GetMovementZ()
    {
        //return new Vector3(0, 0, 0);
        return 2 * horizontalCoefficient * (transform.position.x - midpoint.x) * speed * tempSpeedMultiplier * new Vector3(0, 0, 1);
    }

    //Aiming:
    //  Negative if not aiming
    //  0 if setting a max
    //  1 if setting a min
    //
    //  MUST PASS A NUMBER FOR AIMPOS IF AIMING IS NOT NEGATIVE
    //
    //  tempSpeedMult
    //
    public void NextTarget(int aiming = -1, float aimPos = -1, float tempSpeedMult = 1)
    {
        //ToggleCollider();
        FlashCollider();
        tempSpeedMultiplier = tempSpeedMult;

        nextTarget++;
        if (nextTarget >= targets.Count)
            nextTarget = 0;
        
        Vector3 newTargetPoint = targets[nextTarget].GenerateTarget(aiming, aimPos);
        directAxis = newTargetPoint - transform.position;
        Vector3.Normalize(directAxis);
        normal = Vector3.Cross(directAxis, new Vector3(0, 0, 1)).normalized;
        midpoint = Vector3.Lerp(newTargetPoint, transform.position, 0.5f);
        targetPoint = newTargetPoint;

        SpeedGrow();
        CalculateParameters();
        GameObject projection = Instantiate(projectionPrefab, transform.position, Quaternion.identity);
        projection.GetComponent<ShuttlecockProjection>().Initialize(speed, targetPoint, directAxis, normal, midpoint, horizontalCoefficient, verticalCoefficient);
        //Invoke("ToggleCollider", 0.1f);
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void SpeedGrow()
    {
        if (speed < speedCap)
            speed += speedGrowth;
    }

    public void AddSpeed(float growth)
    {
        speed += growth;
    }

    public void ResetSpeed()
    {
        speed = baseSpeed;
    }

    public bool IsMoving()
    {
        return moving;
    }

    public void SetMoving(bool newStatus)
    {
        moving = newStatus;
    }

    private void ToggleCollider()
    {
        GetComponent<Collider>().enabled = !GetComponent<Collider>().enabled;
    }

    public void FlashCollider()
    {
        ToggleCollider();
        Invoke("ToggleCollider", 0.1f);
    }

    public float GetDamageMultiplier()
    {
        return tempSpeedMultiplier;
    }
}
