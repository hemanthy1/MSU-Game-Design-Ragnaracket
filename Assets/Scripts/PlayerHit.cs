using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHit : MonoBehaviour
{
    private InputAction hitAction;

    private InputAction flipAction;
    private BoxCollider racketCollider;
    private AudioSource swingSound;

    private GameObject racket;

    private GameObject player;

    private GameObject shuttlecock;

    private bool noSpam=false;

    private int perfectHits=0;
    [SerializeField]
    private int pointsToSuper = 100;
    [SerializeField]
    private int pointsFromPerfectHit = 10;
    private int totalPoints = 0;

    private SliderUI superMeter;

    bool flipped=false;

    private void Start()
    {
        superMeter = GameObject.Find("SuperMeter").GetComponent<SliderUI>();
        superMeter.SetMax(pointsToSuper);
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
        flipAction = new InputAction("Flip", InputActionType.Button, "<Mouse>/rightButton");
        flipAction.performed += OnFlip;
        flipAction.Enable();
        hitAction.performed += OnHit;
        hitAction.Enable();
        
    }

    public void OnFlip(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

            if (!flipped)
            {
                racket.SetActive(false);
                racket = player.transform.Find("RacketFlipped").gameObject;
                racket.SetActive(true);
                flipped = true;
            }
            else if (flipped)
            {
                racket.SetActive(false);
                racket = player.transform.Find("Racket").gameObject;
                racket.SetActive(true);
                flipped = false;
            }
        }
    }

    public void OnHit(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            swingSound.Play();
            
            float distance = Vector3.Distance(player.transform.position, shuttlecock.transform.position);
            

            if(!noSpam)
            {
                if(distance<2.0f)
                {
                    shuttlecock.GetComponent<ShuttlecockMotion>().NextTarget(0,1.0f,2f);
                    VolleyManager.instance.AddVolley();
                    
                }
                else if(distance<3.0f)
                {
                    shuttlecock.GetComponent<ShuttlecockMotion>().NextTarget(0,1.0f,1.5f);
                    VolleyManager.instance.AddVolley();
                    
                }
                else if (distance <5.0f)
                {
                    
                    shuttlecock.GetComponent<ShuttlecockMotion>().NextTarget(0,1.0f,1.25f);
                    VolleyManager.instance.AddVolley();
                    
                }
                
                
                

                if (distance<3.55f)
                {
                    perfectHits++;
                    totalPoints += pointsFromPerfectHit;
                    if (totalPoints > pointsToSuper)
                        totalPoints = pointsToSuper;
                    superMeter.UpdateValue(totalPoints);
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
        flipAction.performed -= OnFlip;
        
        hitAction.Disable();
        flipAction.Disable();
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
