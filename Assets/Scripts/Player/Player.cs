using UnityEngine;

[RequireComponent(typeof(Controls))]

public class Player : MonoBehaviour
{
    private PlayerMovementStateMachine movementStateMachine;
    private PlayerCombatStateMachine combatStateMachine;

    public PlayerInput input { get; private set;}
    public new Rigidbody2D rigidbody2D { get; private set;}

    [field: Header("References")]
    [field: SerializeField] public PlayerSO data { get; private set;}

    public CombatManager combatManager;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        combatManager = GetComponent<CombatManager>();

        movementStateMachine = new PlayerMovementStateMachine(this);
        combatStateMachine = new PlayerCombatStateMachine(this);
    }
    
    private void Start()
    {
        movementStateMachine.ChangeState(movementStateMachine.IdilingState);
        combatStateMachine.ChangeState(combatStateMachine.InactiveState);
    }

    private void Update()
    {
        movementStateMachine.HandleInput();
        movementStateMachine.Update();

        combatStateMachine.HandleInput();
        combatStateMachine.Update();
    }

    private void FixedUpdate()
    {
        movementStateMachine.PhysicsUpdate();

        combatStateMachine.PhysicsUpdate();
    }
}
