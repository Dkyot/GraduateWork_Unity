using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controls))]

public class Player : MonoBehaviour
{
    private PlayerMovementStateMachine movementStateMachine;
    public PlayerInput input { get; private set;}
    public new Rigidbody2D rigidbody2D { get; private set;}

    [field: Header("References")]
    [field: SerializeField] public PlayerSO data { get; private set;}
    
    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        movementStateMachine = new PlayerMovementStateMachine(this);
    }
    
    private void Start()
    {
        movementStateMachine.ChangeState(movementStateMachine.IdilingState);
    }

    private void Update()
    {
        movementStateMachine.HandleInput();
        movementStateMachine.Update();

        //Debug.Log(movementStateMachine.reusableData.shouldWalk);
    }

    private void FixedUpdate()
    {
        movementStateMachine.PhysicsUpdate();
    }
}
