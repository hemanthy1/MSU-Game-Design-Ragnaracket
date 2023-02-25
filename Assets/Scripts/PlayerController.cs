using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UI.CanvasScaler;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float stunTime = 1f;
    private Vector2 move;

    //Rigidbody rigid;

    private InputAction dashAction;

    private float activeMoveSpeed;
    public float dashSpeed;

    public float dashLength = 0.1f, dashCooldown = 1f;

    private float dashCounter;
    private float dashCoolCounter;
    private DashIndicator dashIndicator;

    private bool superActive = false;

    private AudioSource[] gruntSounds;
    private AudioSource stunSound;

    private Animator anim;

    private void Start()
    {
        anim = transform.Find("kratos_anim").GetComponent<Animator>();
        gruntSounds = transform.Find("Audio").Find("Grunts").gameObject.GetComponentsInChildren<AudioSource>();
        stunSound = transform.Find("Audio").Find("Stun").GetComponent<AudioSource>();
        dashIndicator = GameObject.Find("DashIndicator").GetComponent<DashIndicator>();
        dashIndicator.SetMax(dashCooldown);
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
        AnimMoving(move.x > 0.1f || move.x < -0.1f);

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
        if (!superActive && dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
            dashIndicator.SetValue(dashCooldown - dashCoolCounter);
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
            if ((superActive || dashCoolCounter <= 0) && dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
                if (!superActive)
                    dashIndicator.Use();
                MaybePlayGrunt();
                AnimDash();
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

    // Enable player control
    private void EnableInput()
    {
        GetComponent<PlayerInput>().ActivateInput();
    }

    // Disable player control
    private void DisableInput()
    {
        GetComponent<PlayerInput>().DeactivateInput();
    }

    // Temporarily stun player
    public void Stun()
    {
        Debug.Log("Stun");
        DisableInput();
        GetComponent<PlayerJump>().ForceFall();
        Invoke("EnableInput", stunTime);
        stunSound.Play();
    }

    // Activate super
    public void ActivateSuper()
    {
        superActive = true;

        PlayRandomGrunt();

        if (dashCoolCounter > 0)
        {
            dashCoolCounter = 0;
            dashIndicator.SetValue(dashCooldown - dashCoolCounter);
        }
    }

    // Deactivate super
    public void DeactivateSuper()
    {
        superActive = false;
    }

    // Play random grunt sound
    public void PlayRandomGrunt()
    {
        int index = Random.Range(0, 3);
        gruntSounds[index].Play();
    }

    public void MaybePlayGrunt()
    {
        if (Random.Range(0f, 1f) <= 0.33f)
            PlayRandomGrunt();
    }

    //
    // Animator functions
    //

    public void AnimJump()
    {
        anim.SetBool("InAir", true);
    }

    public void AnimLand()
    {
        anim.SetBool("InAir", false);
    }

    public void AnimDash()
    {
        anim.SetTrigger("Dash");
    }

    public void AnimRage()
    {
        anim.SetTrigger("Enrage");
    }

    public void AnimHit()
    {
        anim.SetTrigger("Hit");
    }

    public void AnimMoving(bool val)
    {
        anim.SetBool("Moving", val);
    }

    public void AnimLeft(bool val)
    {
        anim.SetBool("LeftHand", val);
    }
}
