using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActiveState : PlayerGroundedCombatState
{
    public PlayerActiveState(PlayerCombatStateMachine playerCombatStateMachine) : base(playerCombatStateMachine)
    {
    }
}
