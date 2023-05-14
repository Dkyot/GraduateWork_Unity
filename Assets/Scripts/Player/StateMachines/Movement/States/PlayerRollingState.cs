using UnityEngine;

public class PlayerRollingState : PlayerGroundedState
{
    private PlayerRollData rollData;
    private bool isRolling;
    private float rollSpeed;
    
    public PlayerRollingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        rollData = movementData.rollData;
    }

    #region IState Methods
    public override void Enter() {
        base.Enter();
        isRolling = true;
        rollSpeed = rollData.rollSpeed;

        stateMachine.player.combatManager?.Dash();
    }

    public override void Update() {
        base.Update();

        OnMove();
    }

    #endregion

    #region Input Mehtods
    protected override void OnMove() {
        if (!isRolling) {
            if (stateMachine.reusableData.movementInput != Vector2.zero)
                base.OnMove();
            else
                stateMachine.ChangeState(stateMachine.IdilingState);
        }

    }
    #endregion

    #region Main Methods
    protected override void Move()
    {
        if (isRolling) {
            rollSpeed -= rollSpeed * rollData.rollSpeedMultiplier * Time.deltaTime;
            if (rollSpeed < rollData.rollSpeedMinimum)
                isRolling = false;
        }
        stateMachine.reusableData.speedModifier = rollSpeed;
        base.Move();
    }
    #endregion
}