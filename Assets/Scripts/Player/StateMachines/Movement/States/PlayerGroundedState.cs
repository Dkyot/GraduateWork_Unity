using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class PlayerGroundedState : PlayerMovementState
{
    public PlayerGroundedState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    #region Reusable Mehtods
    protected override void AddInputActionsCallbacks() {
        base.AddInputActionsCallbacks();

        stateMachine.player.input.playerActions.Movement.canceled += OnMovementCanceled;

        //stateMachine.player.input.playerActions.Roll.started += OnRollStarted;
        //stateMachine.player.input.playerActions.Roll.canceled += OnRollCanceled;////////
    }

    protected override void RemoveInputActionsCallbacks() {
        base.RemoveInputActionsCallbacks();

        stateMachine.player.input.playerActions.Movement.canceled -= OnMovementCanceled;

        //stateMachine.player.input.playerActions.Roll.started -= OnRollStarted;
        //stateMachine.player.input.playerActions.Roll.canceled -= OnRollCanceled;////////
    }

    protected virtual void OnMove()
    {
        if (stateMachine.reusableData.shouldWalk) {
            stateMachine.ChangeState(stateMachine.WallkingState);
            return;
        }

        stateMachine.ChangeState(stateMachine.RunningState);
    }
    #endregion

    #region Input Mehtods 
    protected virtual void OnMovementCanceled(InputAction.CallbackContext context) {
        stateMachine.ChangeState(stateMachine.IdilingState);
    }

    // protected virtual void OnRollStarted(InputAction.CallbackContext context) {
    //     stateMachine.ChangeState(stateMachine.RollingState);
    // }

    // protected virtual void OnRollCanceled(InputAction.CallbackContext context) {
    //     //stateMachine.ChangeState(stateMachine.IdilingState);
    // }
    #endregion
}
