using UnityEngine;

public class ShootProjectiles : MonoBehaviour
{
    [SerializeField] private PoolManager poolManager;
    
    [SerializeField]
    private GameObject bulletPhysics;
    [SerializeField]
    private PlayerInput input;
    [SerializeField]
    private Transform shootPosition;

    private void Start() {
        //poolManager.Preload(bulletPhysics, 5);
    }

    public void Shoot() {
        GameObject bullet = poolManager.Spawn(bulletPhysics, transform.position, Quaternion.identity);
        Vector3 direction = (GetPointerInput() - (Vector2)transform.position).normalized;
        bullet.GetComponent<BulletPhysics>().Setup(poolManager, direction, gameObject.layer);
    }

    private Vector2 GetPointerInput() {
        Vector3 mousePos = input.inputActions.InGamePlayerInput.PointerPosition.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
