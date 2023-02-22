using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Catapult : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float spawnInterval = 20.0f;

    private void Start()
    {
        InvokeRepeating("SpawnObject", 0.0f, spawnInterval);
    }

    private void SpawnObject()
    {
        Instantiate(objectToSpawn, transform.position, Quaternion.identity);
    }
}
