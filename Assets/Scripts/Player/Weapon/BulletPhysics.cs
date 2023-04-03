using UnityEngine;

public class BulletPhysics : MonoBehaviour
{
    private PoolManager poolManager;
    
    private new Rigidbody2D rigidbody2D;
    [SerializeField]
    private float moveSpeed = 100f;
    [SerializeField]
    private int damageAmount = 2;

    private LayerMask parentLayer;
    [SerializeField]
    private LayerMask wallLayer;

    private float ttl = 2f;
    private float timer = 0;

    public void Setup(PoolManager poolManager, Vector3 shootDir, LayerMask layer) {
        if (rigidbody2D == null) {
            rigidbody2D = GetComponent<Rigidbody2D>();
            if (rigidbody2D == null) return;
        }

        if (this.poolManager == null)
           this.poolManager = poolManager;

        parentLayer = layer;

        rigidbody2D.AddForce(shootDir * moveSpeed, ForceMode2D.Impulse);
        transform.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(shootDir));

        timer = 0;
    }

    private void Update() {
        timer += Time.deltaTime;
        if (timer >= ttl) {
            poolManager.Despawn(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (parentLayer == collider.gameObject.layer) return;
        if ((wallLayer.value & (1 << collider.gameObject.layer)) != 0) {
            poolManager.Despawn(this.gameObject);
            return;
        }
        
        CharacterStats target = collider.gameObject.GetComponent<CharacterStats>();
        if (target != null) {
            target.GetHealthSystem().Damage(damageAmount);
            poolManager.Despawn(this.gameObject);
        }
    }

    private float GetAngleFromVectorFloat(Vector3 direction) {
        direction = direction.normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360;
        return angle;
    }
}
