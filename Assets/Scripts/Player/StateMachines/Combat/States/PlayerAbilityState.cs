using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityState : PlayerActiveState
{
    public PlayerAbilityState(PlayerCombatStateMachine playerCombatStateMachine) : base(playerCombatStateMachine)
    {
    }

    private float timer;

    #region IState methods
    public override void Enter() {
        base.Enter();
        
        timer = 0;
        
        stateMachine.reusableData.shouldUseAbility = true;

        stateMachine.player.combatManager?.Ability();

    }

    public override void Update() {
        base.Update();

        timer += Time.deltaTime;

        if (timer > 1f) {
            if (stateMachine.reusableData.shouldAttack == true)
                stateMachine.ChangeState(stateMachine.AttackingState);
            else 
                stateMachine.ChangeState(stateMachine.InactiveState);
        }
    }

    public override void Exit() {
        base.Exit();

        stateMachine.reusableData.shouldUseAbility = false;
    }
    #endregion

    #region Input mehtods
    protected override void OnAbilityToggleStarted(InputAction.CallbackContext context) {
        //
    }
    #endregion
}
