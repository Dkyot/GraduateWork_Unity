using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementState : IState
{
    protected PlayerMovementStateMachine stateMachine;
    protected Vector2 movementInput;

    protected float baseSpeed = 5f;
    protected float speedModifier = 1f;

    public PlayerMovementState(PlayerMovementStateMachine playerMovementStateMachine) {
        stateMachine = playerMovementStateMachine;
    }
    
    #region  IState Methods
    public virtual void Enter() {
        Debug.Log("State: " + GetType().Name);
    }

    public virtual void Exit() {
        
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
        movementInput = stateMachine.player.input.playerActions.Movement.ReadValue<Vector2>();
    }

    private void Move() {
        Vector3 moveDirection = GetMovementInputDirection();
        float movementSpeed = GetMovementSpeed();

        stateMachine.player.rigidbody2D.velocity = moveDirection * movementSpeed;
    }

    #endregion
    
    #region Reusable Mehtods
    protected Vector3 GetMovementInputDirection() {
        return new Vector3(movementInput.x, movementInput.y, 0f);
    }

    protected float GetMovementSpeed() {
        return baseSpeed * speedModifier;
    }

    protected Vector3 GetPlayerVelocity() {
        return stateMachine.player.rigidbody2D.velocity;
    }
    #endregion
}
