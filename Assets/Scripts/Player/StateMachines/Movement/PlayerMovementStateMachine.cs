public class PlayerMovementStateMachine : StateMachine
{
    public Player player { get; private set;}
    public PlayerStateReusableData reusableData { get; private set;}
    
    public PlayerIdilingState IdilingState   { get; private set;}
    public PlayerRunningState RunningState   { get; private set;}
    public PlayerRollingState RollingState   { get; private set;}

    public PlayerMovementStateMachine(Player player) {
        this.player = player;
        reusableData = new PlayerStateReusableData();

        IdilingState = new PlayerIdilingState(this);
        RunningState = new PlayerRunningState(this);
        RollingState = new PlayerRollingState(this);
    }
}
