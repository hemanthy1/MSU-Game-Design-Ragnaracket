using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHit : MonoBehaviour
{
    private InputAction hitAction;
    public void OnEnable()
    {
        hitAction = new InputAction("Hit", InputActionType.Button, "<Keyboard>/y");
        hitAction.performed += OnHit;
        hitAction.Enable();
    
    
        
    }

    public void OnHit(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
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
}
