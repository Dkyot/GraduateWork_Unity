using UnityEngine;

public class ShootProjectiles : MonoBehaviour
{
    [SerializeField] private PoolManager poolManager;
    
    [SerializeField]
    private GameObject bulletPhysics;
    [SerializeField]
    private Transform shootPosition;

    public Vector2 PointerInput { get => pointerInput; set => pointerInput = value; }

    private Vector2 pointerInput;

    private void Start() {
        poolManager = FindObjectOfType<PoolManager>();
    }

    public void Shoot() {
        GameObject bullet = poolManager.Spawn(bulletPhysics, transform.position, Quaternion.identity);
        bullet.GetComponent<BulletPhysics>().Setup(poolManager, pointerInput, gameObject.layer);
    }
}
