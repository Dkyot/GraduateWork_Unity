using UnityEngine;

public class WeaponHolderRotation : MonoBehaviour
{
    public Vector2 PointerInput { get => pointerInput; set => pointerInput = value; }

    private Vector2 pointerInput;

    void Update()
    {
        transform.right = Vector3.Lerp(transform.right, pointerInput, 0.1f);

        Vector2 scale = transform.localScale;
        if (pointerInput.x < 0)
            scale.y = -1;
        else
            scale.y = 1;
       
        if (transform.rotation == Quaternion.Euler(0, 180, 0))
            scale.y = 1;


        transform.localScale = scale;    
    }
}
