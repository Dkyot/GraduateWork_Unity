public class PlayerGroundedCombatState : PlayerCombatState
{
    public PlayerGroundedCombatState(PlayerCombatStateMachine playerCombatStateMachine) : base(playerCombatStateMachine)
    {
    }

    #region Reusable mehtods
    protected virtual void OnAct() {
        if (stateMachine.reusableData.shouldUseAbility == false && stateMachine.reusableData.shouldAttack == true)
            stateMachine.ChangeState(stateMachine.AttackingState);
    }

    protected virtual void OnInactive() {
        if (stateMachine.reusableData.shouldUseAbility == false && stateMachine.reusableData.shouldAttack == false)
            stateMachine.ChangeState(stateMachine.InactiveState);
    }
    #endregion
}
