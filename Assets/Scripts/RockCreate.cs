using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RockCreate : MonoBehaviour
{ 

    public GameObject rock;

    private GameObject throwRock;

    private RockThrust thrust;


    // Start is called before the first frame update
    void Start()
    {
        throwRock = Instantiate(rock, transform);
        //throwRock.transform.SetParent(transform, false);
        thrust = throwRock.GetComponent<RockThrust>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!thrust.RockAnimating())
        {
            throwRock = null;
            Instantiate(rock, transform);
            //throwRock = transform.GetChild(0).gameObject;
        }

        
    }
}
