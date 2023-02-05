using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float speed = 14.5f;
    Camera playerCamera;
    GameObject player;
    GameObject playArea;
    Mesh playAreaMesh;
    GameObject LeftSide;
    GameObject RightSide;
    float playAreaLength;
    Rigidbody playerBody;

    // Start is called before the first frame update
    void Start()
    {
        
        playerCamera = GetComponent<Camera>();
        player = GameObject.Find("Player");
        playArea = GameObject.Find("PlayArea");
        LeftSide = GameObject.Find("LeftCollider");
        RightSide = GameObject.Find("RightCollider");
        playAreaLength = Mathf.Abs(LeftSide.transform.position.x - RightSide.transform.position.x);
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
        if (player.transform.position.x > (playAreaLength/2 - 0.2f * (playAreaLength)) ||
            player.transform.position.x < (-playAreaLength / 2 + 0.2f * (playAreaLength)))
        {
            if (Mathf.Abs(playerBody.velocity.magnitude) > 17)
            {
                MoveCamera(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            }
            
        }
        else
        {
            MoveCamera(-transform.position.x, -transform.position.z);
        }
    }

    void MoveCamera(float x, float y)
    {
        Vector3 movementAmount = new Vector3(x, 0, y) * speed * Time.deltaTime;
        transform.Translate(movementAmount);
    }
}
