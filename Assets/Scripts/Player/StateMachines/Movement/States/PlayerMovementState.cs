using UnityEngine;

public class PlayerMovementState : IState
{
    protected PlayerMovementStateMachine stateMachine;

    protected PlayerGroundedData movementData;

    public PlayerMovementState(PlayerMovementStateMachine playerMovementStateMachine) {
        stateMachine = playerMovementStateMachine;

        movementData = stateMachine.player.data.groundedData;
    }
    
    #region  IState methods
    public virtual void Enter() {
        AddInputActionsCallbacks();
    }

    public virtual void Exit() {
        RemoveInputActionsCallbacks();
    }

    public virtual void HandleInput() {
        ReadMovementInput();
    }

    public virtual void Update() {
        //
    }
    
    public virtual void PhysicsUpdate() {
        Move();
    }
    #endregion

    #region Main methods
    private void ReadMovementInput() {
        stateMachine.statesData.movementInput = stateMachine.player.input.playerActions.Movement.ReadValue<Vector2>();
    }

    protected virtual void Move() {
        Vector3 moveDirection = GetMovementInputDirection();
        float movementSpeed = GetMovementSpeed();

        stateMachine.player.rigidbody2D.velocity = moveDirection * movementSpeed;
    }

    #endregion
    
    #region Reusable mehtods
    protected Vector3 GetMovementInputDirection() {
        return new Vector3(stateMachine.statesData.movementInput.x, stateMachine.statesData.movementInput.y, 0f);
    }

    protected float GetMovementSpeed() {
        return movementData.baseSpeed * stateMachine.statesData.speedModifier;
    }

    protected Vector3 GetPlayerVelocity() {
        return stateMachine.player.rigidbody2D.velocity;
    }

    protected void ResetVelocity() {
        stateMachine.player.rigidbody2D.velocity = Vector3.zero;
    }
    #endregion

    #region Input actions methods
    protected virtual void AddInputActionsCallbacks() {
        //
    }

    protected virtual void RemoveInputActionsCallbacks() {
        //
    }
    #endregion

}
