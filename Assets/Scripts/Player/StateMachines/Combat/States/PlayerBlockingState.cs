using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBlockingState : PlayerActiveState
{
    public PlayerBlockingState(PlayerCombatStateMachine playerCombatStateMachine) : base(playerCombatStateMachine)
    {
    }

    #region IState Methods
    public override void Update()
    {
        base.Update();

        // if (stateMachine.reusableData.shouldAttack == true || stateMachine.reusableData.shouldBlock == true) {
        //     return;
        // }

        OnInactive();
    }
    #endregion

    #region Input Mehtods
    protected override void OnAttackToggleStarted(InputAction.CallbackContext context)
    {
        //base.OnAttackToggleStarted(context);
        Debug.Log("нельзя атаковать");
    }
    #endregion
}
