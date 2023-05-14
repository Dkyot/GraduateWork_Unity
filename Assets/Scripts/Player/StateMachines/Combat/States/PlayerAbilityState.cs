using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityState : PlayerActiveState
{
    public PlayerAbilityState(PlayerCombatStateMachine playerCombatStateMachine) : base(playerCombatStateMachine)
    {
    }

    private float timer;

    #region IState Methods
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
            if (stateMachine.reusableData.shouldBlock == true)
                stateMachine.ChangeState(stateMachine.BlockingState);
            else 
                stateMachine.ChangeState(stateMachine.InactiveState);
        }
    }

    public override void Exit() {
        base.Exit();

        stateMachine.reusableData.shouldUseAbility = false;
    }
    #endregion

    #region Input Mehtods
    protected override void OnAbilityToggleStarted(InputAction.CallbackContext context) {
        //
    }
    #endregion
}
