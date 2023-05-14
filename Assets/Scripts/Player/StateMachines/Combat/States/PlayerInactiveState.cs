public class PlayerInactiveState : PlayerGroundedCombatState
{
    public PlayerInactiveState(PlayerCombatStateMachine playerCombatStateMachine) : base(playerCombatStateMachine)
    {
    }
    
    #region IState Methods
    public override void Update()
    {
        base.Update();

        if (stateMachine.reusableData.shouldAttack == false && stateMachine.reusableData.shouldBlock == false) {
            return;
        }

        OnAct();
    }
    #endregion
}
