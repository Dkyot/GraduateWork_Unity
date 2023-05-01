using UnityEngine;

public class OnCollisionKey : LootBase
{
    [SerializeField] private KeyColors key;
    
    private void OnCollisionEnter2D(Collision2D collider) {
        if (collider.gameObject.CompareTag("Player")) {
            KeyManager manager = collider.gameObject.GetComponentInChildren<KeyManager>();
            if (manager != null) {
                manager.PickKey(key);
                Destroy(gameObject);
            }
        }
    }
}
