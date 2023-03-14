using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private InputActionReference movement, attack, pointerPositiion;

    public Vector2 movementInput { get; private set;}
    public Vector2 mousePosition { get; private set;}

    public event EventHandler OnAttack;

    private void OnEnable() {
        attack.action.performed += PerformAttack;
    }

    private void OnDisable() {
        attack.action.performed -= PerformAttack;
    }

    void Update() {
        movementInput = movement.action.ReadValue<Vector2>();
        mousePosition = GetPointerInput();
    }

    private Vector2 GetPointerInput() {
        Vector3 mousePosition = pointerPositiion.action.ReadValue<Vector2>();
        mousePosition.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
    
    private void PerformAttack(InputAction.CallbackContext obj) {
        OnAttack?.Invoke(this, EventArgs.Empty);
    }
}


