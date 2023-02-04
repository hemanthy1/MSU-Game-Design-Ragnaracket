using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    Camera playerCamera;
    GameObject player;
    GameObject playArea;
    Mesh playAreaMesh;
    GameObject LeftSide;
    GameObject RightSide;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GetComponent<Camera>();
        player = GameObject.Find("Player");
        playArea = GameObject.Find("PlayArea");
        LeftSide = GameObject.Find("LeftCollider");
        RightSide = GameObject.Find("RightCollider");
        
    }

    // Update is called once per frame
    void Update()
    {
        CameraPositioning();
    }

    void CameraPositioning()
    {
        
        //Use the collider's position to find the edges and do the math from there. 
        if (Mathf.Abs(player.transform.position.x) > 0) 
        {
            MoveCamera(player.transform.position.x - 0 * Time.deltaTime);
        }
    }

    void MoveCamera(float delta)
    {
        playerCamera.transform.Translate(delta, 0, 0);
    }
}
