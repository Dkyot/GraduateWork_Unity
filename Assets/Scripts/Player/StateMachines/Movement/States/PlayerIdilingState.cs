using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdilingState : PlayerGroundedState
{
    public PlayerIdilingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    #region  IState methods
    public override void Enter() {
        base.Enter();

        stateMachine.reusableData.speedModifier = 0f;
        ResetVelocity();
    }

    public override void Update() {
        base.Update();

        if (stateMachine.reusableData.movementInput == Vector2.zero) {
            return;
        }

        OnMove();
    }
    #endregion

    #region Input mehtods
    protected override void OnRollStarted(InputAction.CallbackContext context) {
        //
    }
    #endregion
}
