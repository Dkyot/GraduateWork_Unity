using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatStateMachine : StateMachine
{
    public Player player { get; private set;}
    public PlayerStateReusableData reusableData { get; private set;}
    
    public PlayerAttackingState AttackingState  { get; private set;}
    public PlayerBlockingState BlockingState    { get; private set;}
    public PlayerInactiveState InactiveState    { get; private set;}

    public PlayerCombatStateMachine(Player player) {
        this.player = player;
        reusableData = new PlayerStateReusableData();

        AttackingState = new PlayerAttackingState(this);
        BlockingState = new PlayerBlockingState(this);
        InactiveState = new PlayerInactiveState(this);
    }
}
