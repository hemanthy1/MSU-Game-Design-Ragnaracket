using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Catapult : MonoBehaviour
{
    public GameObject objectToSpawn;
    private TrebuchetAnimation anim;
    private Transform launchPoint;
    public float spawnInterval = 5.0f;
    public float intervalVariation = 1.0f;
    public float intervalGrowth = 0.25f;
    public float intervalMin = 0.5f;

    private void Start()
    {
        anim = transform.Find("trebuchet_anim").GetComponent<TrebuchetAnimation>();
        launchPoint = transform.Find("LaunchPoint");
        Invoke("RandomizeInterval", 0.0f);
    }

    private void RandomizeInterval()
    {
        float variation = Random.Range(0.0f, intervalVariation);
        Invoke("Throw", variation);
    }

    private void Throw()
    {
        anim.AnimationStart();
        Invoke("SpawnObject", 1.8f);
    }

    private void SpawnObject()
    {
        Instantiate(objectToSpawn, launchPoint.position, Quaternion.identity);
        Invoke("RandomizeInterval", spawnInterval);
    }

    public void SpeedUp()
    {
        spawnInterval -= intervalGrowth;
        if (spawnInterval < intervalMin)
            spawnInterval = intervalMin;
    }
}
