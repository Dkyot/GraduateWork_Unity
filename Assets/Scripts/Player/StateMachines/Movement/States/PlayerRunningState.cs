public class PlayerRunningState : PlayerMovingState
{
    public PlayerRunningState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    #region IState methods
    public override void Enter() {
        base.Enter();

        stateMachine.reusableData.speedModifier = movementData.runData.speedModifier;
    }
    #endregion
}
