using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    [SerializeField ] private KeyColors key;
    private KeyManager playerKeys;

    public UnityEvent Open;

    private void OnTriggerEnter2D (Collider2D collision) {
        if (collision.gameObject.CompareTag("Player"))
            playerKeys = collision.gameObject.GetComponentInChildren<KeyManager>();
    }

    public void TryToUnlock() {
        if (playerKeys == null) return;

        switch (key) {
            case KeyColors.Red:   if (playerKeys.redKey)   Open.Invoke(); Debug.Log("fd");break;
            case KeyColors.Green: if (playerKeys.greenKey) Open.Invoke(); break;
            case KeyColors.White: if (playerKeys.whiteKey) Open.Invoke(); break;
            default: return;
        }
    }
}
