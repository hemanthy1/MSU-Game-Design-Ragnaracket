using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://gist.github.com/ditzel/68be36987d8e7c83d48f497294c66e08

public class RockThrust : MonoBehaviour
{
    protected float parabolaAnimation;

<<<<<<< Updated upstream
    private GameObject player;
    
    private Vector3 destination;

    private float minZ = -15f;
    private float maxZ = 15f;
=======
    ParabolaController parabolaController;

    Vector3 startPoint;

    //[SerializeField]
    //private Vector3 destination = new Vector3(16, 3.5f, 0);
>>>>>>> Stashed changes

    //Update this script so that it picks a random destination on the player plane to launch to

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< Updated upstream
        player = GameObject.Find("Player");
        destination = new Vector3(player.transform.position.x, player.transform.position.y, Random.Range(minZ, maxZ));
=======
        parabolaController = GetComponent<ParabolaController>();
        startPoint = transform.position;
        //parabolaController.ParabolaRoot = transform.parent.GetChild(1).gameObject;
>>>>>>> Stashed changes
        
    }

    // Update is called once per frame
    void Update()
    {
        //RockHandler(parabolaController.Animation);

<<<<<<< Updated upstream
        //parabolaAnimation = parabolaAnimation % 5;

        transform.position = MathParabola.Parabola(transform.position, destination, 5, parabolaAnimation / 5);

        //If the transform.position.y is less than a certain threshold, destroy it. 
        if (transform.position.y < -1f)
        {
            Destroy(gameObject);
        }
        
=======
        //parabolaAnimation += Time.deltaTime;

        //parabolaAnimation = parabolaAnimation % 10f;

        //throwRock.transform.position = MathParabola.Parabola(Vector3.zero, Vector3.right * 10f, 10f, parabolaAnimation / 10f);

        Debug.Log("I'm following Parabola");

        parabolaController.FollowParabola();

>>>>>>> Stashed changes
    }

    public bool RockAnimating()
    {
        return parabolaController.Animation;
    }

    //void OnTriggerEnter(Collider collision)
    //{
    //    transform.Translate(startPoint);

    //}

}

