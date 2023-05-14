public class PlayerInactiveState : PlayerGroundedCombatState
{
    public PlayerInactiveState(PlayerCombatStateMachine playerCombatStateMachine) : base(playerCombatStateMachine)
    {
    }
    
    #region IState Methods
    public override void Update()
    {
        base.Update();

        if (stateMachine.reusableData.shouldUseAbility == false && stateMachine.reusableData.shouldAttack == false) {
            return;
        }

        OnAct();
    }
    #endregion
}
