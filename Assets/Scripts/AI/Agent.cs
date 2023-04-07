using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    //private AgentAnimations agentAnimations;
    private AgentMover agentMover;

    //private WeaponParent weaponParent;

    //[SerializeField]
    //private InputActionReference movement, attack, pointerPosition;

    public Vector2 PointerInput { get => pointerInput; set => pointerInput = value; }
    public Vector2 MovementInput { get => movementInput; set => movementInput = value; }

    private Vector2 pointerInput, movementInput;

    private void Update()
    {
        //pointerInput = GetPointerInput();
        //movementInput = movement.action.ReadValue<Vector2>().normalized;

        agentMover.MovementInput = movementInput;
        //weaponParent.PointerPosition = pointerInput;
        AnimateCharacter();
    }

    private void OnEnable()
    {
        //attack.action.performed += PerformAttack;
    }

    private void OnDisable()
    {
        //attack.action.performed -= PerformAttack;
    }

    // private Vector2 GetPointerInput()
    // {
    //     Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
    //     mousePos.z = Camera.main.nearClipPlane;
    //     return Camera.main.ScreenToWorldPoint(mousePos);
    // }

    public void PerformAttack()
    {
        //weaponParent.Attack();
    }

    private void Awake()
    {
        //agentAnimations = GetComponentInChildren<AgentAnimations>();
        //weaponParent = GetComponentInChildren<WeaponParent>();
        agentMover = GetComponent<AgentMover>();
    }

    private void AnimateCharacter()
    {
        Vector2 lookDirection = pointerInput - (Vector2)transform.position;
        //agentAnimations.RotateToPointer(lookDirection);
        //agentAnimations.PlayAnimation(movementInput);
    }
}
