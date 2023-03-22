using UnityEngine;

public class MeleeAttackDetection : MonoBehaviour
{
    [SerializeField]
    private Transform circleOrigin;
    [SerializeField]
    private float radius;

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Vector3 position = circleOrigin.position;
        Gizmos.DrawWireSphere(position, radius);
    }

    public void DetectColliders() {
        foreach(Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position, radius)) {
            //Debug.Log(collider.name);
            CharacterStats health = collider.GetComponent<CharacterStats>();
            if (health == null || this.gameObject.layer == collider.gameObject.layer) continue;
            health.GetHealthSystem().Damage(2);
            //Debug.Log("enemy hp: " + health.GetHealthSystem().GetCurrentHP());
        }
    }
}
