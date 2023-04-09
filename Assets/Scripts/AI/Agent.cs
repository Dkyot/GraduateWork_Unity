using UnityEngine;

public class Agent : MonoBehaviour
{
    private AgentMover agentMover;

    public Vector2 PointerInput { get => pointerInput; set => pointerInput = value; }
    public Vector2 MovementInput { get => movementInput; set => movementInput = value; }

    private Vector2 pointerInput, movementInput;

    private void Update() {
        agentMover.MovementInput = movementInput;
    }

    public void PerformAttack() {
        //weaponParent.Attack();
    }

    private void Awake() {
        agentMover = GetComponent<AgentMover>();
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, pointerInput.normalized);
    }
}
