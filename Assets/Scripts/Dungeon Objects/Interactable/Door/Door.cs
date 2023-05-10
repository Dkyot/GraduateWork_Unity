using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField ] private KeyColors key;
    private KeyManager playerKeys;

    private void OnTriggerEnter2D (Collider2D collision) {
        if (collision.gameObject.CompareTag("Player"))
            playerKeys = collision.gameObject.GetComponentInChildren<KeyManager>();
    }

    public void TryToUnlock() {
        if (playerKeys == null) return;

        switch (key) {
            case KeyColors.Red:   if (playerKeys.redKey)   Open(); break;
            case KeyColors.Green: if (playerKeys.greenKey) Open(); break;
            case KeyColors.White: if (playerKeys.whiteKey) Open(); break;
            default: return;
        }
    }

    private void Open() {
        FindObjectOfType<SceneSwitcher>().SwitchScene();
    }
}
