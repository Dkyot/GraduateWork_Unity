using UnityEngine;

public class BulletPhysics : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;
    [SerializeField]
    private float moveSpeed = 100f;
    [SerializeField]
    private int damageAmount = 2;

    private LayerMask parentLayer;
    [SerializeField]
    private LayerMask wallLayer;

    public void Setup(Vector3 shootDir, LayerMask layer) {
        rigidbody2D = GetComponent<Rigidbody2D>();
        if (rigidbody2D == null) return;

        parentLayer = layer;

        rigidbody2D.AddForce(shootDir * moveSpeed, ForceMode2D.Impulse);
        transform.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(shootDir));
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (parentLayer == collider.gameObject.layer) return;
        if ((wallLayer.value & (1 << collider.gameObject.layer)) != 0) {
            Destroy(gameObject);
            return;
        }
        
        CharacterStats target = collider.gameObject.GetComponent<CharacterStats>();
        if (target != null) {
            target.GetHealthSystem().Damage(damageAmount);
            Debug.Log("enemy hp: " + target.GetHealthSystem().GetCurrentHP());
            Destroy(gameObject);
        }
    }

    private float GetAngleFromVectorFloat(Vector3 direction) {
        direction = direction.normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360;
        return angle;
    }
}
