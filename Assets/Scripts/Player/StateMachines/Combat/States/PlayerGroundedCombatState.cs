public class PlayerGroundedCombatState : PlayerCombatState
{
    public PlayerGroundedCombatState(PlayerCombatStateMachine playerCombatStateMachine) : base(playerCombatStateMachine)
    {
    }

    #region Reusable Mehtods
    protected virtual void OnAct() {
        if (stateMachine.reusableData.shouldAttack == false && stateMachine.reusableData.shouldBlock == true)
            stateMachine.ChangeState(stateMachine.BlockingState);
    }

    protected virtual void OnInactive() {
        if (stateMachine.reusableData.shouldAttack == false && stateMachine.reusableData.shouldBlock == false)
            stateMachine.ChangeState(stateMachine.InactiveState);
    }
    #endregion
}
