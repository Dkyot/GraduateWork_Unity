public class PlayerGroundedCombatState : PlayerCombatState
{
    public PlayerGroundedCombatState(PlayerCombatStateMachine playerCombatStateMachine) : base(playerCombatStateMachine)
    {
    }

    #region Reusable mehtods
    protected virtual void OnAct() {
        if (stateMachine.statesData.shouldUseAbility == false && stateMachine.statesData.shouldAttack == true)
            stateMachine.ChangeState(stateMachine.AttackingState);
    }

    protected virtual void OnInactive() {
        if (stateMachine.statesData.shouldUseAbility == false && stateMachine.statesData.shouldAttack == false)
            stateMachine.ChangeState(stateMachine.InactiveState);
    }
    #endregion
}
