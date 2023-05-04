using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    [SerializeField] private PlayerInput input;
    
    private bool isInRange;
    private CircleCollider2D circle;
    private float radius;
    [SerializeField] private GameObject obj;
    public UnityEvent interactAction;

    [SerializeField] private bool onFinishDestroy;
    [SerializeField] private bool isDisposable;
    private bool isUsed;

    private bool isMobile;

    private void Start() {
        if (Application.isMobilePlatform) isMobile = true;
        circle = GetComponentInChildren<CircleCollider2D>();
        radius = circle.radius;
        //Debug.Log(radius);
    }

    private void Update() {
        if (input == null) return;
        if (!isMobile)
            PCInteraction();
    }

    private void MobileInteraction(InputAction.CallbackContext ctx) { 
        if (!isInRange) return;
        //Debug.Log(input.transform.position);
        Vector2 touchC = input.playerActions.MobileInteract.ReadValue<Vector2>();
        Vector2 touchP = Camera.main.ScreenToWorldPoint(touchC);
        
        float dist = Vector2.Distance(touchP, transform.position);
        //Debug.Log(dist);
        if (dist > radius) return;
        
        RaycastHit2D hit = Physics2D.Raycast(touchP, Vector2.zero);
        if (hit.collider != null) {
            if (hit.collider.tag == "Interactable") Interact();
        }
    }

    private void PCInteraction() {
        if (input.playerActions.Interact.IsPressed()) {
                Interact();
        }
    }

    private void Interact() {
        if(isUsed) return;
        if (isDisposable) isUsed = true;
        interactAction?.Invoke();
        if (onFinishDestroy) Destroy(obj);
    }

    private void OnTriggerEnter2D (Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            isInRange = true;
            if (input == null) {
                input = collision.GetComponent<PlayerInput>();
                if (isMobile)
                    input.playerActions.MobileInteract.performed += ctx => MobileInteraction(ctx);
            }
        }
    }

    private void OnTriggerExit2D (Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            isInRange = false;
            if (isMobile)
                input.playerActions.MobileInteract.performed -= ctx => MobileInteraction(ctx);
            input = null;
        }
    }

}
