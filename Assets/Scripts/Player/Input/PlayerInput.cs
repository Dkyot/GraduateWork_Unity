using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
