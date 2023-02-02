using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class PlayerWallkingState : PlayerMovingState
{
    public PlayerWallkingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    #region  IState Methods
    public override void Enter() {
        base.Enter();

        stateMachine.reusableData.speedModifier = movementData.walkData.speedModifier;
    }
    #endregion

    #region Input Mehtods
    protected override void OnWalkToggleStarted(InputAction.CallbackContext context) {
        base.OnWalkToggleStarted(context);

        stateMachine.ChangeState(stateMachine.RunningState);
    }
    #endregion
}
