using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public Controls inputActions { get; private set;}
    public Controls.InGamePlayerInputActions playerActions { get; private set;}

    public UnityEvent<Vector2> OnPointerInput;

    //public Vector2 touchPos {get; private set;}

    private void Awake() {
        inputActions = new Controls();
        playerActions = inputActions.InGamePlayerInput;
    }

    private bool mobileDebug = false;

    private void Start() {
        //playerActions.MobileInteract.performed += ctx => Touch(ctx);
    }

    // private void Touch(InputAction.CallbackContext ctx)
    // {
    //     Vector2 touchC = playerActions.MobileInteract.ReadValue<Vector2>();
    //     touchPos = Camera.main.ScreenToWorldPoint(touchC);
    //     //Debug.Log(touchPos);
    // }

    private void Update() {
        if (Application.isMobilePlatform || mobileDebug)
            OnPointerInput?.Invoke(inputActions.InGamePlayerInput.PointerPosition.ReadValue<Vector2>());
        else
            OnPointerInput?.Invoke((GetPointerInput() - (Vector2)transform.position).normalized);
    }

    private void OnEnable() {
        //playerActions.MobileInteract.performed += ctx => Touch(ctx);
        inputActions.Enable();
        
    }

    private void OnDisable() {
        //playerActions.MobileInteract.performed -= ctx => Touch(ctx);
        inputActions.Disable();
        
    }

    public void DisableActionFor (InputAction action, float seconds) {
        StartCoroutine(DisableAction(action, seconds));
    }

    private IEnumerator DisableAction (InputAction action, float seconds) {
        action.Disable();
        yield return new WaitForSeconds(seconds);
        action.Enable();
    }

    private Vector2 GetPointerInput() {
        Vector3 mousePos = inputActions.InGamePlayerInput.PointerPosition.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
