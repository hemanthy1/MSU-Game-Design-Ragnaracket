using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHit : MonoBehaviour
{
    private InputAction hitAction;
    private InputAction flipAction;
    private InputAction superAction;
    private BoxCollider racketCollider;
    private AudioSource swingSound;
    private AudioSource rockSound;
    private AudioSource hitSound;

    private GameObject racket;
    private GameObject racketModel;
    private GameObject racketModelFlipped;

    private GameObject player;

    private GameObject shuttlecock;

    private DeflectCounter deflectCounter;

    private Light superAura;

    private bool noSpam=false;

    public int perfectHits=0;
    [SerializeField]
    private int pointsToSuper = 100;
    [SerializeField]
    private int pointsFromPerfectHit = 10;
    private int totalPoints = 0;

    private SliderUI superMeter;

    private bool flipped=false;

    private bool superActive = false;
    [SerializeField]
    private float superDamageMultiplier = 1.5f;
    [SerializeField]
    private float superTimeLimit = 5f;

    private int rockDeflects=0;

    public float rockMult=0.25f;





    private void Start()
    {
        deflectCounter = GameObject.Find("DeflectIndicator").GetComponent<DeflectCounter>();
        superAura = GetComponent<Light>();
        superMeter = GameObject.Find("SuperMeter").GetComponent<SliderUI>();
        superMeter.SetMax(pointsToSuper);
        swingSound = transform.Find("Audio").Find("Swing").GetComponent<AudioSource>();
        rockSound = transform.Find("Audio").Find("RockHit").GetComponent<AudioSource>();
        hitSound = transform.Find("Audio").Find("Hit").GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player");
        shuttlecock=GameObject.FindWithTag("Shuttlecock");
        if (player != null)
        {
            racket = player.transform.Find("Racket").gameObject;
            racketCollider = racket.GetComponent<BoxCollider>();
            racketModel = GameObject.Find("AxModel").gameObject;
            racketModelFlipped = GameObject.Find("AxModelFlipped").gameObject;
            racketModelFlipped.SetActive(false);
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
        superAction = new InputAction("Super", InputActionType.Button, "<Keyboard>/q");
        superAction.performed += OnSuper;
        superAction.Enable();
    }

    public void OnFlip(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

            if (!flipped)
            {
                racket.SetActive(false);
                racket = transform.Find("RacketFlipped").gameObject;
                racket.SetActive(true);

                racketModel.SetActive(false);
                racketModelFlipped.SetActive(true);

                flipped = true;

                GetComponent<PlayerController>().AnimLeft(true);
            }
            else if (flipped)
            {
                racket.SetActive(false);
                racket = transform.Find("Racket").gameObject;
                racket.SetActive(true);

                racketModelFlipped.SetActive(false);
                racketModel.SetActive(true);

                flipped = false;

                GetComponent<PlayerController>().AnimLeft(false);
            }
        }
    }

    public void OnHit(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            
            bool haveCooldown = true;
            float distance = Vector3.Distance(player.transform.position, shuttlecock.transform.position);

            if(!noSpam)
            {
                swingSound.Play();
                GetComponent<PlayerController>().AnimHit();

                GameObject rock = GameObject.FindWithTag("Rock");
                if (rock != null)
                {
                    float rockDistance = Vector3.Distance(player.transform.position, rock.transform.position);
                    if (rockDistance < 4.5f)
                    {
                        Destroy(rock);
                        rockDeflects++;
                        Debug.Log("Rocks deflected: " + rockDeflects);
                        deflectCounter.UpdateCounter(rockDeflects);
                        haveCooldown = false;
                        rockSound.Play();
                        GetComponent<PlayerController>().MaybePlayGrunt();
                    }
                }

                if (distance<2.0f)
                {
                    float damage = 2f * (superActive ? superDamageMultiplier : 1) + Mathf.Max(1, rockDeflects * rockMult);
                    Debug.Log("Multiplier: " + damage);
                    shuttlecock.GetComponent<ShuttlecockMotion>().NextTarget(0, 1.0f, damage);
                    rockDeflects = 0;
                    deflectCounter.ResetCounter();
                    VolleyManager.instance.AddVolley();
                    hitSound.Play();
                    GetComponent<PlayerController>().MaybePlayGrunt();
                }
                else if(distance<3.0f)
                {
                    float damage = 1.5f * (superActive ? superDamageMultiplier : 1) + Mathf.Max(1, rockDeflects * rockMult);
                    Debug.Log("Multiplier: " + damage);
                    shuttlecock.GetComponent<ShuttlecockMotion>().NextTarget(0, 1.0f, damage);
                    rockDeflects = 0;
                    deflectCounter.ResetCounter();
                    VolleyManager.instance.AddVolley();
                    hitSound.Play();
                    GetComponent<PlayerController>().MaybePlayGrunt();
                }
                else if (distance <5.0f)
                {
                    float damage = 1.25f * (superActive ? superDamageMultiplier : 1) * Mathf.Max(1, rockDeflects * rockMult);
                    Debug.Log("Multiplier: " + damage);
                    shuttlecock.GetComponent<ShuttlecockMotion>().NextTarget(0, 1.0f, damage);
                    rockDeflects = 0;
                    deflectCounter.ResetCounter();
                    VolleyManager.instance.AddVolley();
                    hitSound.Play();
                    GetComponent<PlayerController>().MaybePlayGrunt();
                }
                
                
                

                if (distance<3.55f)
                {
                    perfectHits++;
                    if (!superActive)
                    {
                        totalPoints += pointsFromPerfectHit;
                        if (totalPoints > pointsToSuper)
                            totalPoints = pointsToSuper;
                        superMeter.UpdateValue(totalPoints);
                    }
                    //Debug.Log("Perfect hits: " + perfectHits);
                }
            }
            noSpam=haveCooldown;


            //racketCollider.enabled = true;
            StartCoroutine(TurnOffAfterDelay(0.25f));
        
           
            
        }
    }
    public void OnDisable()
    {
        hitAction.performed -= OnHit;
        flipAction.performed -= OnFlip;
        superAction.performed -= OnSuper;
        
        hitAction.Disable();
        flipAction.Disable();
        superAction.Disable();
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

    public void OnSuper(InputAction.CallbackContext context)
    {
        if (totalPoints >= pointsToSuper)
        {
            superActive = true;
            GetComponent<PlayerController>().ActivateSuper();
            superMeter.EmptyOverTime(superTimeLimit);
            totalPoints = 0;
            superAura.enabled = true;
            GetComponent<PlayerController>().AnimRage();

            Invoke("DeactivateSuper", superTimeLimit);
        }
    }

    private void DeactivateSuper()
    {
        superActive = false;
        GetComponent<PlayerController>().DeactivateSuper();
        superAura.enabled = false;
    }
}
