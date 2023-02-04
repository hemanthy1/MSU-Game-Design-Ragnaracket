using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{

    private CharacterController characterController;
    private Vector3 playerVelocity;
    private bool isGrounded;

    [SerializeField] private float jumpHeight = 5.0f; 

    private bool jumpPressed = false;

    private float gravity = -9.81f;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MovementJump();
    }

    void MovementJump()
    {
        isGrounded = characterController.isGrounded;
        //If on the ground, stop the vertical movement
        if (isGrounded)
        {
            playerVelocity.y = 0.0f;
        }

        if (jumpPressed && isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * gravity * -1.0f);
            jumpPressed = false;
        }

        playerVelocity.y += gravity * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
    }

    public void OnJump()
    {
        Debug.Log("Jump pressed");
        
        if (characterController.velocity.y == 0)
        {
            jumpPressed = true;
        }
    }
}
