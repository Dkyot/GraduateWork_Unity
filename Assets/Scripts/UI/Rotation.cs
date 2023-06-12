using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1f;
    
    private void Start() {
        Time.timeScale = 1;
    }
    
    private void Update() {
        gameObject.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
