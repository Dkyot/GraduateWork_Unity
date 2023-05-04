using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] private PlayerInput input;
    
    private bool isInRange;
    [SerializeField] private KeyCode interactKey;
    [SerializeField] private GameObject obj;
    public UnityEvent interactAction;

    [SerializeField] private bool onFinishDestroy;
    
    [SerializeField] private bool isDisposable;
    private bool isUsed;

    void Update() {
        if (input == null) return;
        if (isInRange && !isUsed) {
            if (input.playerActions.Interact.IsPressed()) {
                if (isDisposable) isUsed = true;
                interactAction?.Invoke();
                if (onFinishDestroy) Destroy(obj);
            }
        }
    }

    private void OnTriggerEnter2D (Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            isInRange = true;
            if (input == null) input = collision.GetComponent<PlayerInput>();
        }
    }

    private void OnTriggerExit2D (Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) isInRange = false;
    }
}
