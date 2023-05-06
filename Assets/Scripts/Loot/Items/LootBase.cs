using UnityEngine;

public abstract class LootBase : MonoBehaviour
{
    protected new Rigidbody2D rigidbody2D;
    
    protected float time = 0;
    protected float timeToStop = 0.4f;
    protected bool stopped = false;

    protected bool hasTarget;
    protected Transform targetTransform;
    protected float magnetSpeed = 15f;

    [SerializeField] 
    protected bool isMagnetic = false;

    private void Start() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        if (rigidbody2D.velocity == Vector2.zero) stopped = true;
    }

    private void FixedUpdate() {
        DropingMove();
        if (stopped && isMagnetic) MagnetToTarget();
    }

    private void DropingMove() {
        if (rigidbody2D.velocity == Vector2.zero || stopped) return;
        if (time > timeToStop) {
            rigidbody2D.velocity = Vector2.zero;
            stopped = true;
        }
        else {
            rigidbody2D.velocity = rigidbody2D.velocity * 0.9f;
            time += Time.deltaTime;
        }
    }

    private void MagnetToTarget() {
        if (hasTarget) {
            Vector2 targetDirection = (targetTransform.position - transform.position).normalized;
            rigidbody2D.velocity = new Vector2(targetDirection.x, targetDirection.y) * magnetSpeed;
        }
    }

    public void SetTarget(Transform transform) {
        targetTransform = transform;
        hasTarget = true;
    }
}
