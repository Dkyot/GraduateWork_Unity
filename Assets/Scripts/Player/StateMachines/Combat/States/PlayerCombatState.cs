using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombatState : IState
{
    protected PlayerCombatStateMachine stateMachine;

    public PlayerCombatState(PlayerCombatStateMachine playerCombatStateMachine) {
        stateMachine = playerCombatStateMachine;
    }
    
    #region  IState methods
    public virtual void Enter() {
        AddInputActionsCallbacks();
    }

    public virtual void Exit() {
        RemoveInputActionsCallbacks();
    }

    public virtual void HandleInput() {
        //
    }

    public virtual void PhysicsUpdate() {
        //
    }

    public virtual void Update(){
        //
    }
    #endregion

    bool mobileDebug = false;

    #region Input actions methods
    protected virtual void AddInputActionsCallbacks() {
        if (Application.isMobilePlatform || mobileDebug) {
            stateMachine.player.input.playerActions.PointerPosition.started += OnAttackToggleStarted;
            stateMachine.player.input.playerActions.PointerPosition.canceled += OnAttackToggleStarted;
        }
        else {
            stateMachine.player.input.playerActions.Attack.started += OnAttackToggleStarted;
            stateMachine.player.input.playerActions.Attack.canceled += OnAttackToggleStarted;
        }

        stateMachine.player.input.playerActions.Ability.started += OnAbilityToggleStarted;
    }

    protected virtual void RemoveInputActionsCallbacks() {
        if (Application.isMobilePlatform || mobileDebug) {
            stateMachine.player.input.playerActions.PointerPosition.started -= OnAttackToggleStarted;
            stateMachine.player.input.playerActions.PointerPosition.canceled -= OnAttackToggleStarted;
        }
        else {
            stateMachine.player.input.playerActions.Attack.started -= OnAttackToggleStarted;
            stateMachine.player.input.playerActions.Attack.canceled -= OnAttackToggleStarted;
        }

        stateMachine.player.input.playerActions.Ability.started -= OnAbilityToggleStarted;
    }
    #endregion

    #region Input mehtods
    protected virtual void OnAbilityToggleStarted(InputAction.CallbackContext context) {
        stateMachine.ChangeState(stateMachine.AbilityState);
    }

    protected virtual void OnAttackToggleStarted(InputAction.CallbackContext context) {
        if (context.started) stateMachine.statesData.shouldAttack = true;
        else stateMachine.statesData.shouldAttack = false;
    }
    #endregion
}
