using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //float speed = 6.0f;
    GameObject player;
    GameObject LeftSide;
    GameObject RightSide;
    float playAreaLength;
    Rigidbody playerBody;

    [SerializeField]
    private GameObject rotatePoint;

    private Vector3 currentPos;
    private Vector3 previousPos;

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
        currentPos = player.transform.position;
        CameraPositioning();
        previousPos = currentPos;
    }

    void CameraPositioning()
    {
        //Rigid rotating of camera
        //if (player.transform.position.z < -10)
        //{
        //    Vector3 newRotation = new Vector3(0, -5, -1);
        //    transform.rotation = Quaternion.Euler(newRotation);
        //    if (player.transform.position.y > 8)
        //    {
        //        newRotation = new Vector3(0, -5, -2);
        //        transform.rotation = Quaternion.Euler(newRotation);
        //    }
        //}

        //else if (player.transform.position.z > 10)
        //{
        //    Vector3 newRotation = new Vector3(0, 5, -1);
        //    transform.rotation = Quaternion.Euler(newRotation);
        //    if (player.transform.position.y > 8)
        //    {
        //        newRotation = new Vector3(0, 5, -2);
        //        transform.rotation = Quaternion.Euler(newRotation);
        //    }
        //}

        //else
        //{
        //    Vector3 newRotation = new Vector3(0, 0, -1);
        //    transform.rotation = Quaternion.Euler(newRotation);
        //}

        transform.RotateAround(rotatePoint.transform.position, Vector3.down, (currentPos.z - previousPos.z)/3);
    }

}
