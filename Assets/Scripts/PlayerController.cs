using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UI.CanvasScaler;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Vector2 move;

    //Rigidbody rigid;

    private InputAction dashAction;

    private float activeMoveSpeed;
    public float dashSpeed;

    public float dashLength = 0.5f, dashCooldown = 1f;

    private float dashCounter;
    private float dashCoolCounter;

    private void Start()
    {
        activeMoveSpeed = speed;
    }

    private void Awake()
    {
        //rigid = GetComponent<Rigidbody>();
        //rigid.interpolation = RigidbodyInterpolation.Interpolate;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void MovePlayer()
    {

        Vector3 movement = new Vector3(0f, 0f, move.x);

        transform.Translate(movement * activeMoveSpeed * Time.deltaTime, Space.World);

        //if (Time.time > 2)
        //{
        //    Debug.Log("Hi I'm here");
        //}
        //rigid.MovePosition(transform.position + (movement * activeMoveSpeed * Time.deltaTime));
    }

    
    void FixedUpdate()
    {
        MovePlayer();
        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                activeMoveSpeed = speed;
                dashCoolCounter = dashCooldown;
            }
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (dashCoolCounter <= 0 && dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
            }
        }

    }

    public void OnEnable()
    {
        dashAction = new InputAction("Dash", InputActionType.Button, "<Keyboard>/shift");
        dashAction.performed += OnDash;
        dashAction.Enable();

    }

    public void OnDisable()
    {
        dashAction.performed -= OnDash;

        dashAction.Disable();
    }
}
