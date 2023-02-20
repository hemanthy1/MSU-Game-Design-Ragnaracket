using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float speed = 6.0f;
    GameObject player;
    GameObject LeftSide;
    GameObject RightSide;
    float playAreaLength;
    Rigidbody playerBody;

    // Start is called before the first frame update
    void Start()
    {
        //transform.Translate(15, -1, 0);
        player = GameObject.Find("Player");
        LeftSide = GameObject.Find("LeftCollider");
        RightSide = GameObject.Find("RightCollider");
        playAreaLength = Mathf.Abs(LeftSide.transform.position.z - RightSide.transform.position.z);
        playerBody = player.GetComponent<Rigidbody>();


    }

    // Update is called once per frame
    void Update()
    {
        CameraPositioning();
        
    }

    void CameraPositioning()
    {
        
        //Use the collider's position to find the edges and do the math from there. 
        if (player.transform.position.z > (playAreaLength/2 - 0.2f * (playAreaLength)) ||
            player.transform.position.z < (-playAreaLength / 2 + 0.2f * (playAreaLength)))
        {
            if (Mathf.Abs(playerBody.velocity.magnitude) > 20)
            {
                MoveCamera(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            }
            
        }
        else
        {
            MoveCamera(-transform.position.z, 0);
        }
    }

    void MoveCamera(float x, float y)
    {
        Vector3 movementAmount = new Vector3(y, 0, x) * speed * Time.deltaTime;
        transform.Translate(movementAmount);
    }
}
