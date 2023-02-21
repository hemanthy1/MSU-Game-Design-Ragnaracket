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
        
        if (player.transform.position.z < -10)
        {
            Vector3 newRotation = new Vector3(0, -5, -1);
            transform.rotation  = Quaternion.Euler(newRotation);
            if (player.transform.position.y > 8)
            {
                newRotation = new Vector3(0, -5, -2);
                transform.rotation = Quaternion.Euler(newRotation);
            }
        }

        else if (player.transform.position.z > 10)
        {
            Vector3 newRotation = new Vector3(0, 5, -1);
            transform.rotation = Quaternion.Euler(newRotation);
            if (player.transform.position.y > 8)
            {
                newRotation = new Vector3(0, 5, -2);
                transform.rotation = Quaternion.Euler(newRotation);
            }
        }

        else
        {
            Vector3 newRotation = new Vector3(0, 0, -1);
            transform.rotation = Quaternion.Euler(newRotation);
        }
    }

    //void MoveCamera(float x, float y)
    //{
    //    Vector3 movementAmount = new Vector3(y, 0, x) * speed * Time.deltaTime;
    //    transform.Translate(movementAmount);
    //}
}
