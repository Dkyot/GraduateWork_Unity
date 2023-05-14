using UnityEngine;

public class MeleeAttackDetection : MonoBehaviour
{
    [SerializeField] private Transform circleOrigin;
    [SerializeField] private float radius;

    [SerializeField] private int damage = 2;

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Vector3 position = circleOrigin.position;
        Gizmos.DrawWireSphere(position, radius);
    }

    public void DetectColliders() {
        foreach(Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position, radius)) {
            CharacterStats health = collider.GetComponent<CharacterStats>();
            if (health == null || this.gameObject.layer == collider.gameObject.layer) continue;
            health.GetHealthSystem().Damage(damage);
        }
    }
}
