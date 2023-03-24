using UnityEngine;

public class HeartHeal : MonoBehaviour
{
    [SerializeField] 
    private int healAmount;
    private new Rigidbody2D rigidbody2D;
    private float time = 0;
    private float timeToStop = 0.5f;
    private bool stopped = false;

    private bool hasTarget;
    private Vector3 targetPosition;
    private float magnetSpeed = 5f;

    private void OnCollisionEnter2D(Collision2D collider) {
        CharacterStats player = collider.gameObject.GetComponent<CharacterStats>();
         if (player != null) {
            player.GetHealthSystem().Heal(healAmount);
            Destroy(gameObject);
        }
    }

    private void Start() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        if (rigidbody2D.velocity == Vector2.zero) stopped = true;
    }
    
    private void FixedUpdate() {
        //Debug.Log(stopped);
        DropingMove();
        if (stopped) MagnetToTarget();
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
            Vector2 targetDirection = (targetPosition - transform.position).normalized;
            rigidbody2D.velocity = new Vector2(targetDirection.x, targetDirection.y) * magnetSpeed;
        }
    }

    public void SetTarget(Vector3 position) {
        targetPosition = position;
        hasTarget = true;
    }
}