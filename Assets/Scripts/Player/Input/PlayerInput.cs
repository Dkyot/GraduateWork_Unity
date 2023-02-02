using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public Controls inputActions { get; private set;}
    public Controls.InGamePlayerInputActions playerActions { get; private set;}

    private void Awake() {
        inputActions = new Controls();
        playerActions = inputActions.InGamePlayerInput;
    }

    private void OnEnable() {
        inputActions.Enable();
    }

    private void OnDisable() {
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
}
