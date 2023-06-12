using UnityEngine;

public class SinRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1f;
    
    private void Start() {
        
    }

    private void Update() {
        float sinRotation = rotationSpeed * Time.deltaTime *  Mathf.Sin(Time.time);
        gameObject.transform.Rotate(0, 0, sinRotation);
    }
}
