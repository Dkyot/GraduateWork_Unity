using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public Controls inputActions { get; private set;}
    public Controls.InGamePlayerInputActions playerActions { get; private set;}

    public UnityEvent<Vector2> OnPointerInput;

    private void Awake() {
        inputActions = new Controls();
        playerActions = inputActions.InGamePlayerInput;
    }

    private void Update() {
        if (Application.isMobilePlatform)
            OnPointerInput?.Invoke(inputActions.InGamePlayerInput.PointerPosition.ReadValue<Vector2>());
        else
            OnPointerInput?.Invoke((GetPointerInput() - (Vector2)transform.position).normalized);
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

    private Vector2 GetPointerInput() {
        Vector3 mousePos = inputActions.InGamePlayerInput.PointerPosition.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
