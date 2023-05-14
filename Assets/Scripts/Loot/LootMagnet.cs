using UnityEngine;

public class LootMagnet : MonoBehaviour
{
    [SerializeField] private Transform circleOrigin;
    [SerializeField] private float radius;

    [SerializeField] private LayerMask layerMask;

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Vector3 position = circleOrigin.position;
        Gizmos.DrawWireSphere(position, radius);
    }

    private void Update() {
        DetectColliders();
    }

    public void DetectColliders() {
        foreach(Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position, radius)) {
            if ((layerMask.value & (1 << collider.gameObject.layer)) == 0) continue;
            
            if(collider.TryGetComponent<LootBase>(out LootBase drop)) {
                drop.SetTarget(transform);
            }
        }
    }
}
