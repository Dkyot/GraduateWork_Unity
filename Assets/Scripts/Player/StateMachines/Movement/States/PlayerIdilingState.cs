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

        stateMachine.statesData.speedModifier = 0f;
        ResetVelocity();
    }

    public override void Update() {
        base.Update();

        if (stateMachine.statesData.movementInput == Vector2.zero) {
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
