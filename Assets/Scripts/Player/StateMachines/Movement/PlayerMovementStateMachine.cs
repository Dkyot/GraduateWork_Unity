using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementStateMachine : StateMachine
{
    public Player player { get; private set;}
    
    public PlayerIdilingState IdilingState   { get; private set;}
    public PlayerWallkingState WallkingState { get; private set;}
    public PlayerRunningState RunningState   { get; private set;}

    public PlayerMovementStateMachine(Player player) {
        this.player = player;

        IdilingState = new PlayerIdilingState(this);
        WallkingState = new PlayerWallkingState(this);
        RunningState = new PlayerRunningState(this);
    }
}
