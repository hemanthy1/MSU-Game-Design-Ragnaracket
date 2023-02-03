using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Vector2 move;

    private void Awake()
    {

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void MovePlayer()
    {
        Vector3 movement = new Vector3(move.x, 0f, 0f);

        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }

    
    void Update()
    {
        MovePlayer();
    }
}
