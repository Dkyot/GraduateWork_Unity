using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackingState : PlayerActiveState
{
    public PlayerAttackingState(PlayerCombatStateMachine playerCombatStateMachine) : base(playerCombatStateMachine)
    {
    }

    private float timer;

    #region IState methods
    public override void Enter() {
        base.Enter();
        
        timer = 0;

        stateMachine.player.combatManager?.AttackHandler();
    }

    public override void Update() {
        base.Update();

        timer += Time.deltaTime;

        if (timer > 0.2f) {
            stateMachine.player.combatManager?.AttackHandler();
            timer = 0;
        }

        OnInactive();
    }
    #endregion

    #region Input Mehtods
    protected override void OnAbilityToggleStarted(InputAction.CallbackContext context) {
        //
    }
    #endregion
}
