using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://gist.github.com/ditzel/68be36987d8e7c83d48f497294c66e08

public class RockThrust : MonoBehaviour
{
    protected float parabolaAnimation;

    public GameObject player;
    
    private Vector3 destination;

    //Update this script so that it picks a random destination on the player plane to launch to

    // Start is called before the first frame update
    void Start()
    {
        destination = player.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        parabolaAnimation += Time.deltaTime;

        parabolaAnimation = parabolaAnimation % 5;

        transform.position = MathParabola.Parabola(transform.position, destination, 5, parabolaAnimation / 5);

        
    }
}

