using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombatState : IState
{
    protected PlayerCombatStateMachine stateMachine;

    public PlayerCombatState(PlayerCombatStateMachine playerCombatStateMachine) {
        stateMachine = playerCombatStateMachine;
    }
    
    #region  IState Methods
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

    #region Reusable Mehtods
    protected virtual void AddInputActionsCallbacks() {
        if (Application.isMobilePlatform || mobileDebug) {
            stateMachine.player.input.playerActions.PointerPosition.started += OnBlockToggleStarted;
            stateMachine.player.input.playerActions.PointerPosition.canceled += OnBlockToggleStarted;
        }
        else {
            stateMachine.player.input.playerActions.Block.started += OnBlockToggleStarted;
            stateMachine.player.input.playerActions.Block.canceled += OnBlockToggleStarted;
        }

        stateMachine.player.input.playerActions.Attack.started += OnAttackToggleStarted;
    }

    protected virtual void RemoveInputActionsCallbacks() {
        if (Application.isMobilePlatform || mobileDebug) {
            stateMachine.player.input.playerActions.PointerPosition.started -= OnBlockToggleStarted;
            stateMachine.player.input.playerActions.PointerPosition.canceled -= OnBlockToggleStarted;
        }
        else {
            stateMachine.player.input.playerActions.Block.started -= OnBlockToggleStarted;
            stateMachine.player.input.playerActions.Block.canceled -= OnBlockToggleStarted;
        }

        stateMachine.player.input.playerActions.Attack.started -= OnAttackToggleStarted;
    }
    #endregion

    #region Input Mehtods
    protected virtual void OnAttackToggleStarted(InputAction.CallbackContext context) {
        stateMachine.ChangeState(stateMachine.AttackingState);
    }

    protected virtual void OnBlockToggleStarted(InputAction.CallbackContext context) {
        if (context.started) stateMachine.reusableData.shouldBlock = true;
        else stateMachine.reusableData.shouldBlock = false;
    }
    #endregion
}
