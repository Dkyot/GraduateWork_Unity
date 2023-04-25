using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombatState : IState
{
    protected PlayerCombatStateMachine stateMachine;

    protected PlayerGroundedData movementData;

    public PlayerCombatState(PlayerCombatStateMachine playerCombatStateMachine) {
        stateMachine = playerCombatStateMachine;

        movementData = stateMachine.player.data.groundedData;// добавить новые данные
    }
    
    #region  IState Methods
    public virtual void Enter()
    {
        //Debug.Log("Combat state: " + GetType().Name);

        AddInputActionsCallbacks();
    }

    public virtual void Exit()
    {
        RemoveInputActionsCallbacks();
    }

    public virtual void HandleInput()
    {
        //ReadCombatInput();
    }

    public virtual void PhysicsUpdate()
    {
        
    }

    public virtual void Update()
    {
        
    }
    #endregion

    #region Main Methods
    // private void ReadCombatInput() {
    //     //stateMachine.reusableData.movementInput = stateMachine.player.input.playerActions.Movement.ReadValue<Vector2>();
    // }
    #endregion

    #region Reusable Mehtods
    protected virtual void AddInputActionsCallbacks() {
        
        

        stateMachine.player.input.playerActions.Block.started += OnBlockToggleStarted;
        stateMachine.player.input.playerActions.Block.canceled += OnBlockToggleStarted;

        if (Application.isMobilePlatform)
            stateMachine.player.input.playerActions.PointerPosition.started += OnAttackToggleStarted;/////////////////////////////////
        else {
            stateMachine.player.input.playerActions.Attack.started += OnAttackToggleStarted;
            //stateMachine.player.input.playerActions.Attack.canceled += OnAttackToggleStarted;
        }
    }

    protected virtual void RemoveInputActionsCallbacks() {
        

        stateMachine.player.input.playerActions.Block.started -= OnBlockToggleStarted;
        stateMachine.player.input.playerActions.Block.canceled -= OnBlockToggleStarted;

        if (Application.isMobilePlatform)
            stateMachine.player.input.playerActions.PointerPosition.started -= OnAttackToggleStarted;/////////////////////////////////
        else {
            stateMachine.player.input.playerActions.Attack.started -= OnAttackToggleStarted;
            //stateMachine.player.input.playerActions.Attack.canceled -= OnAttackToggleStarted;
        }
    }
    #endregion

    #region Input Mehtods
    protected virtual void OnAttackToggleStarted(InputAction.CallbackContext context)
    {
        if (Application.isMobilePlatform) {
            if (stateMachine.player.input.playerActions.PointerPosition.ReadValue<Vector2>() == Vector2.zero) return;
            stateMachine.ChangeState(stateMachine.AttackingState);
        }
        else {
            // if (context.started) stateMachine.reusableData.shouldAttack = true;
            // else stateMachine.reusableData.shouldAttack = false;
            stateMachine.ChangeState(stateMachine.AttackingState);
        }
    }

    protected virtual void OnBlockToggleStarted(InputAction.CallbackContext context)
    {
        if (context.started) stateMachine.reusableData.shouldBlock = true;
        else stateMachine.reusableData.shouldBlock = false;
    }
    #endregion
}
