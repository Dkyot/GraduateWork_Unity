using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActiveState : PlayerGroundedCombatState
{
    public PlayerActiveState(PlayerCombatStateMachine playerCombatStateMachine) : base(playerCombatStateMachine)
    {
    }

    #region IState Methods
    // public override void Update()
    // {
    //     base.Update();

    //     // if (stateMachine.reusableData.shouldAttack == true || stateMachine.reusableData.shouldBlock == true) {
    //     //     return;
    //     // }

    //     // OnInactive();
    // }
    #endregion
}
