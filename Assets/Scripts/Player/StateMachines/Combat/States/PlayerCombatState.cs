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
    public void Enter()
    {
        Debug.Log("Combat state: " + GetType().Name);

        AddInputActionsCallbacks();
    }

    public void Exit()
    {
        RemoveInputActionsCallbacks();
    }

    public void HandleInput()
    {
        ReadCombatInput();
    }

    public void PhysicsUpdate()
    {
        
    }

    public void Update()
    {
        
    }
    #endregion

    #region Main Methods
    private void ReadCombatInput() {
        //stateMachine.reusableData.movementInput = stateMachine.player.input.playerActions.Movement.ReadValue<Vector2>();
    }
    #endregion

    #region Reusable Mehtods
    protected virtual void AddInputActionsCallbacks() {
        stateMachine.player.input.playerActions.Attack.started += OnAttackToggleStarted;
        stateMachine.player.input.playerActions.Attack.canceled += OnAttackToggleStarted;
    }

    protected virtual void RemoveInputActionsCallbacks() {
        stateMachine.player.input.playerActions.Attack.started -= OnAttackToggleStarted;
        stateMachine.player.input.playerActions.Attack.canceled -= OnAttackToggleStarted;
    }
    #endregion

    #region Input Mehtods
    protected virtual void OnAttackToggleStarted(InputAction.CallbackContext context)
    {
        if (context.started) Debug.Log(true);
        else Debug.Log(false);
    }
    #endregion
}
