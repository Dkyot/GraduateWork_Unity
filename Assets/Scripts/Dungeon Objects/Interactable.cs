using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    private bool isInRange;
    [SerializeField] private KeyCode interactKey;
    [SerializeField] private GameObject obj;
    public UnityEvent interactAction;

    [SerializeField] private bool onFinishDestroy;
    
    [SerializeField] private bool isDisposable;
    private bool isUsed;

    void Update() {
        if (isInRange && !isUsed) {
            if (Input.GetKeyDown(interactKey)) {
                if (isDisposable) isUsed = true;
                interactAction?.Invoke();
                if (onFinishDestroy) Destroy(obj);
            }
        }
    }

    private void OnTriggerEnter2D (Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) isInRange = true;
    }

    private void OnTriggerExit2D (Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) isInRange = false;
    }
}
