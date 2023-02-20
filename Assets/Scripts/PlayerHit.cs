using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHit : MonoBehaviour
{
    private InputAction hitAction;
    private BoxCollider racketCollider;
    private AudioSource swingSound;

    private GameObject racket;

    private GameObject player;

    private GameObject shuttlecock;

    private bool noSpam=false;

    private int perfectHits=0;

    private void Start()
    {
        swingSound = transform.Find("Audio").Find("Swing").GetComponent<AudioSource>();
        player= GameObject.FindWithTag("Player");
        shuttlecock=GameObject.FindWithTag("Shuttlecock");
        if (player != null)
        {
            racket = player.transform.Find("Racket").gameObject;
            racketCollider = racket.GetComponent<BoxCollider>();
        }
       
    }
    public void OnEnable()
    {
        hitAction = new InputAction("Hit", InputActionType.Button, "<Mouse>/leftButton");
        hitAction.performed += OnHit;
        hitAction.Enable();
        
    }

    public void OnHit(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            swingSound.Play();
            
            float distance = Vector3.Distance(player.transform.position, shuttlecock.transform.position);
            

            if(!noSpam)
            {
                if(distance<3.0f)
                {
                    shuttlecock.GetComponent<ShuttlecockMotion>().NextTarget(0,0.5f,2);
                }
                else if (distance <5.0f)
                {
                    //left
                    shuttlecock.GetComponent<ShuttlecockMotion>().NextTarget(0,1.0f,2);
                }
                else if(distance<6.5f)
                {
                    //middle
                    shuttlecock.GetComponent<ShuttlecockMotion>().NextTarget(0,1.25f,2);
                }
                else if (distance <8.0f)
                {
                    //right
                    shuttlecock.GetComponent<ShuttlecockMotion>().NextTarget(1,1.5f,2);
                }
                else if (distance<8.9f)
                {
                    shuttlecock.GetComponent<ShuttlecockMotion>().NextTarget(1,1.8f,2);

                }
                
                

                if (distance<3.55f)
                {
                    perfectHits++;

                    Debug.Log("Perfect hits: " + perfectHits);
                }
        }
            noSpam=true;


            //racketCollider.enabled = true;
            StartCoroutine(TurnOffAfterDelay(0.25f));
        
           
            
        }
    }
    public void OnDisable()
    {
        hitAction.performed -= OnHit;
        
        hitAction.Disable();
    }
    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator TurnOffAfterDelay(float delay)
    {

        yield return new WaitForSeconds(delay);
        noSpam = false;
    }

    private IEnumerator StopSpam(float delay)
    {
        yield return new WaitForSeconds(delay);
    }

}
