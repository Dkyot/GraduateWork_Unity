public class PlayerRunningState : PlayerMovingState
{
    public PlayerRunningState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    #region IState Methods
    public override void Enter() {
        base.Enter();

        stateMachine.reusableData.speedModifier = movementData.runData.speedModifier;
    }
    #endregion
}
