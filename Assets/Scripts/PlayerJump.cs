using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{

    private CharacterController characterController;
    private Vector3 playerVelocity;
    private bool isGrounded;

    private bool isJumpReleased;

    [SerializeField] private float jumpHeight = 32.0f; 

    private bool jumpPressed = false;

    [SerializeField] private float gravity = -9.81f;



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
        isJumpReleased = Input.GetButtonUp("Jump");
        //If on the ground, stop the vertical movement
        if (isGrounded)
        {
            playerVelocity.y = 0.0f;
            GetComponent<PlayerController>().AnimLand();
        }

        if (isJumpReleased && playerVelocity.y > 0.0f)
        {
            playerVelocity.y = 0;
        }

        if (jumpPressed && isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * gravity * -1.0f);
            jumpPressed = false;
            GetComponent<PlayerController>().MaybePlayGrunt();
            GetComponent<PlayerController>().AnimJump();
        }

        playerVelocity.y += gravity * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
    }

    public void OnJump()
    {
        //Debug.Log("Jump pressed");
        
        if (characterController.velocity.y == 0)
        {
            jumpPressed = true;
        }
    }

    public void ForceFall()
    {
        playerVelocity.y = 0;
    }
}
