public class PlayerCombatStateMachine : StateMachine
{
    public Player player { get; private set;}
    public PlayerStateReusableData reusableData { get; private set;}
    
    public PlayerAbilityState AbilityState  { get; private set;}
    public PlayerAttackingState AttackingState    { get; private set;}
    public PlayerInactiveState InactiveState    { get; private set;}

    public PlayerCombatStateMachine(Player player) {
        this.player = player;
        reusableData = new PlayerStateReusableData();

        AbilityState = new PlayerAbilityState(this);
        AttackingState = new PlayerAttackingState(this);
        InactiveState = new PlayerInactiveState(this);
    }
}
