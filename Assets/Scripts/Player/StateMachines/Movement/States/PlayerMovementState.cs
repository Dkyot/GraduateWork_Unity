using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementState : IState
{
    protected PlayerMovementStateMachine stateMachine;

    protected PlayerGroundedData movementData;

    public PlayerMovementState(PlayerMovementStateMachine playerMovementStateMachine) {
        stateMachine = playerMovementStateMachine;

        movementData = stateMachine.player.data.groundedData;
    }
    
    #region  IState Methods
    public virtual void Enter() {
        Debug.Log("State: " + GetType().Name);

        AddInputActionsCallbacks();
    }

    public virtual void Exit() {
        RemoveInputActionsCallbacks();
    }

    public virtual void HandleInput() {
        ReadMovementInput();
    }

    public virtual void Update() {
        
    }
    
    public virtual void PhysicsUpdate() {
        Move();
    }
    #endregion

    #region Main Methods
    private void ReadMovementInput() {
        stateMachine.reusableData.movementInput = stateMachine.player.input.playerActions.Movement.ReadValue<Vector2>();
    }

    private void Move() {
        Vector3 moveDirection = GetMovementInputDirection();
        float movementSpeed = GetMovementSpeed();

        stateMachine.player.rigidbody2D.velocity = moveDirection * movementSpeed;
    }

    #endregion
    
    #region Reusable Mehtods
    protected Vector3 GetMovementInputDirection() {
        return new Vector3(stateMachine.reusableData.movementInput.x, stateMachine.reusableData.movementInput.y, 0f);
    }

    protected float GetMovementSpeed() {
        return movementData.baseSpeed * stateMachine.reusableData.speedModifier;
    }

    protected Vector3 GetPlayerVelocity() {
        return stateMachine.player.rigidbody2D.velocity;
    }

    protected void ResetVelocity() {
        stateMachine.player.rigidbody2D.velocity = Vector3.zero;
    }

    protected virtual void AddInputActionsCallbacks() {
        stateMachine.player.input.playerActions.Walk.started += OnWalkToggleStarted;
        stateMachine.player.input.playerActions.Walk.canceled += OnWalkToggleStarted;
    }

    protected virtual void RemoveInputActionsCallbacks() {
        stateMachine.player.input.playerActions.Walk.started -= OnWalkToggleStarted;
        stateMachine.player.input.playerActions.Walk.canceled -= OnWalkToggleStarted;
    }
    #endregion

    #region Input Mehtods
    protected virtual void OnWalkToggleStarted(InputAction.CallbackContext context)
    {
        //stateMachine.reusableData.shouldWalk = !stateMachine.reusableData.shouldWalk;
        if (context.started) stateMachine.reusableData.shouldWalk = true;
        else stateMachine.reusableData.shouldWalk = false;
    }
    #endregion
}
