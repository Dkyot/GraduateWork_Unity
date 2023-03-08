using UnityEngine;

public class WeaponHolderRotation : MonoBehaviour
{
    [SerializeField]
    private PlayerInput input;

    void Update()
    {
        Vector2 direction = (GetPointerInput() - (Vector2)transform.position).normalized;
        //transform.right = direction;
        transform.right = Vector3.Lerp(transform.right, direction, 0.1f);

        Vector2 scale = transform.localScale;
        if (direction.x < 0)
            scale.y = -1;
        else
            scale.y = 1;
       
        if (transform.rotation == Quaternion.Euler(0, 180, 0))
            scale.y = 1;


        transform.localScale = scale;    
    }

    private Vector2 GetPointerInput() {
        Vector3 mousePos = input.inputActions.InGamePlayerInput.PointerPosition.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
