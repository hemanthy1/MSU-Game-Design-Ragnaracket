using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrebuchetAnimation : MonoBehaviour
{
    private MeshRenderer payloadVisual;
    private AudioSource sound;
    private Animator anim;
    [SerializeField]
    private bool launchingShuttlecock = false;
    [SerializeField]
    private bool launchOnStart = false;

    private bool timerOn = false;
    private float payloadTimer = 0f;
    private float payloadHideTime = 1.8f;

    void Start()
    {
        sound = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        payloadVisual = transform.Find("Armature").Find("slingIK").Find("Payload").GetComponent<MeshRenderer>();
        if (launchOnStart)
            AnimationStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerOn)
        {
            payloadTimer += Time.deltaTime;
            if (payloadTimer > payloadHideTime)
            {
                payloadTimer = 0f;
                timerOn = false;
                HidePayload();
                if (launchingShuttlecock)
                    GameObject.FindWithTag("Shuttlecock").GetComponent<ShuttlecockMotion>().SetMoving(true);
            }    
        }
    }

    public void AnimationStart()
    {
        timerOn = true;
        payloadTimer = 0f;
        ShowPayload();
        anim.SetTrigger("Launch");
    }

    public void ShowPayload()
    {
        payloadVisual.enabled = true;
    }

    public void HidePayload()
    {
        payloadVisual.enabled = false;
        sound.Play();
    }
}
