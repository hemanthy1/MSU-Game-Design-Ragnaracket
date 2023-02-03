using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    Camera playerCamera;
    GameObject player;
    GameObject playArea;
    Mesh playAreaMesh;
    Bounds bounds;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GetComponent<Camera>();
        player = GameObject.Find("Player");
        playArea = GameObject.Find("PlayArea");
        playAreaMesh = playArea.GetComponent<MeshFilter>().mesh;
        bounds = playAreaMesh.bounds;
    }

    // Update is called once per frame
    void Update()
    {
        CameraPositioning();
    }

    void CameraPositioning()
    {
        float boundsX = playArea.transform.localScale.x * bounds.size.x;
        //float boundsY = playArea.transform.localScale.y * bounds.size.y;
        //float boundsZ = playArea.transform.localScale.z * bounds.size.z;
        //Need to find some way to get the length of the playarea plane
        if (Mathf.Abs(player.transform.position.x) > boundsX) 
        {
            MoveCamera(player.transform.position.x - boundsX * Time.deltaTime);
        }
    }

    void MoveCamera(float delta)
    {
        playerCamera.transform.Translate(delta, 0, 0);
    }
}
