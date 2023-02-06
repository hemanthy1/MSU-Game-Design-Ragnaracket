using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHit : MonoBehaviour
{
    private InputAction hitAction;
    

    private BoxCollider racketCollider;

    private void Start()
    {
        GameObject player= GameObject.FindWithTag("Player");
        if (player != null)
        {
            GameObject playerRacket = player.transform.Find("Racket").gameObject;
            racketCollider = playerRacket.GetComponent<BoxCollider>();
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
            racketCollider.enabled = true;
            StartCoroutine(TurnOffAfterDelay(1f));
            Debug.Log("Hallelejuah Y is pressed!!!!");
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
        racketCollider.enabled = false;
    }

}
