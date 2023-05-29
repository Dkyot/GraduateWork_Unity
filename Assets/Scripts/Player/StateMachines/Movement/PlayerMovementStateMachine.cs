public class PlayerMovementStateMachine : StateMachine
{
    public Player player { get; private set;}
    public PlayerStatesData statesData { get; private set;}
    
    public PlayerIdilingState IdilingState   { get; private set;}
    public PlayerRunningState RunningState   { get; private set;}
    public PlayerRollingState RollingState   { get; private set;}

    public PlayerMovementStateMachine(Player player) {
        this.player = player;
        statesData = new PlayerStatesData();

        IdilingState = new PlayerIdilingState(this);
        RunningState = new PlayerRunningState(this);
        RollingState = new PlayerRollingState(this);
    }
}
