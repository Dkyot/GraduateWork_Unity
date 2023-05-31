using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1f;

    private void Update() {
        transform.Rotate (0, 0, rotationSpeed * Time.deltaTime);
    }
}
