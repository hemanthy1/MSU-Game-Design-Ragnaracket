using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10.0f;
    public float gravity = 9.8f;
    public float airResistance = 0.1f;
    public Vector3 target;

    private Vector3 velocity;
    private float time;

    void Start()
    {
        velocity = CalculateVelocity(transform.position, target, speed, gravity);
        time = 0.0f;
    }

    void Update()
    {
        time += Time.deltaTime;

        transform.position = CalculatePosition(transform.position, velocity, time, gravity);
        velocity -= velocity * airResistance * Time.deltaTime;
    }

    Vector3 CalculateVelocity(Vector3 source, Vector3 target, float speed, float gravity)
    {
        Vector3 direction = target - source;
        float height = direction.y;
        direction.y = 0;
        float distance = direction.magnitude;
        float angle = Mathf.Min(1, distance / speed) * 45;
        direction.y = distance * Mathf.Tan(angle * Mathf.Deg2Rad);
        distance += height / Mathf.Tan(angle * Mathf.Deg2Rad);

        float velocity = Mathf.Sqrt(distance * gravity / Mathf.Sin(2 * angle * Mathf.Deg2Rad));
        return velocity * direction.normalized;
    }

    Vector3 CalculatePosition(Vector3 source, Vector3 velocity, float time, float gravity)
    {
        return source + velocity * time + 0.5f * gravity * Mathf.Pow(time, 2) * Vector3.down;
    }
}