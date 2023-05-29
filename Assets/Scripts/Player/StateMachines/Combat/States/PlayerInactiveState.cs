public class PlayerInactiveState : PlayerGroundedCombatState
{
    public PlayerInactiveState(PlayerCombatStateMachine playerCombatStateMachine) : base(playerCombatStateMachine)
    {
    }
    
    #region IState methods
    public override void Update()
    {
        base.Update();

        if (stateMachine.statesData.shouldUseAbility == false && stateMachine.statesData.shouldAttack == false) {
            return;
        }

        OnAct();
    }
    #endregion
}
