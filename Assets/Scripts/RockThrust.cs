using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://gist.github.com/ditzel/68be36987d8e7c83d48f497294c66e08

public class RockThrust : MonoBehaviour
{
    protected float parabolaAnimation;

    [SerializeField]
    private Vector3 destination = new Vector3(16, 3.5f, 0);

    //Update this script so that it picks a random destination on the player plane to launch to

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        parabolaAnimation += Time.deltaTime;

        parabolaAnimation = parabolaAnimation % 5f;

        transform.position = MathParabola.Parabola(transform.position, destination, 5f, parabolaAnimation / 5f);
    }
}
