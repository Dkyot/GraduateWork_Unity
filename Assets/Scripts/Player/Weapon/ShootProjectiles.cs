using UnityEngine;

public class ShootProjectiles : MonoBehaviour
{
    [SerializeField]
    private Transform bulletPhysics;
    [SerializeField]
    private PlayerInput input;
    [SerializeField]
    private Transform shootPosition;

    public void Shoot() {
        Transform bulletTransform = Instantiate(bulletPhysics, shootPosition.position, Quaternion.identity);
        Vector3 direction = (GetPointerInput() - (Vector2)transform.position).normalized;
        bulletTransform.GetComponent<BulletPhysics>().Setup(direction, gameObject.layer);
        //Debug.Log(direction);
    }

    private Vector2 GetPointerInput() {
        Vector3 mousePos = input.inputActions.InGamePlayerInput.PointerPosition.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
