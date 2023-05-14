using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBlockingState : PlayerActiveState
{
    public PlayerBlockingState(PlayerCombatStateMachine playerCombatStateMachine) : base(playerCombatStateMachine)
    {
    }

    private float timer;

    #region IState Methods
    public override void Enter() {
        base.Enter();
        
        timer = 0;

        stateMachine.player.combatManager?.AttackHandler();
    }
    #endregion

    #region IState Methods
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
    protected override void OnAttackToggleStarted(InputAction.CallbackContext context) {
        //
    }
    #endregion
}
