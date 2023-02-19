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
        
     
        if (player.transform.position.y > 14f)
        {
            transform.Rotate(0, 0, -(14f - player.transform.position.y / 100000));
            if (player.transform.position.z > 15f)
            {
                transform.Rotate(0, (player.transform.position.z - 15f) / 60, 0);
            }

            if (player.transform.position.z < -15f)
            {
                transform.Rotate(0, (15f + player.transform.position.z) / 60, 0);
            }

            else
            {
                transform.rotation = Quaternion.identity;
            }
        }
    }

    //void MoveCamera(float x, float y)
    //{
    //    Vector3 movementAmount = new Vector3(y, 0, x) * speed * Time.deltaTime;
    //    transform.Translate(movementAmount);
    //}
}
