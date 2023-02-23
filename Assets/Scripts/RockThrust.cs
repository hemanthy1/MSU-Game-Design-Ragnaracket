using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://gist.github.com/ditzel/68be36987d8e7c83d48f497294c66e08

public class RockThrust : MonoBehaviour
{
    protected float parabolaAnimation;


    private GameObject player;

    private Vector3 destination;

    private float minZ = -15f;
    private float maxZ = 15f;

    [Tooltip("Makes rock animation faster or slower. The smaller the value, the faster the rock moves.")]
    public float rockAnimationLength = 3;

    Vector3 startPoint;


    //Update this script so that it picks a random destination on the player plane to launch to

    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.Find("Player");
        destination = new Vector3(player.transform.position.x, player.transform.position.y, Random.Range(minZ, maxZ));

        //parabolaController = GetComponent<ParabolaController>();
        startPoint = transform.position;


    }

    // Update is called once per frame
    void Update()
    {
        parabolaAnimation += Time.deltaTime;

        transform.position = MathParabola.Parabola(transform.position, destination, 15, (parabolaAnimation / rockAnimationLength));

        //If the transform.position.y is less than a certain threshold, destroy it. 
        if (transform.position.y < -1f)
        {
            Destroy(gameObject);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collide");
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().Stun();
            Destroy(gameObject);
        }
    }
}

