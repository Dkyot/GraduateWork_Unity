using UnityEngine;

public class OnCollisionHeal : LootBase
{
    [SerializeField] private int healAmount;
    
    private void OnCollisionEnter2D(Collision2D collider) {
        if (collider.gameObject.CompareTag("Player")) {
            CharacterStats player = collider.gameObject.GetComponent<CharacterStats>();
            if (player != null) {
                player.GetHealthSystem().Heal(healAmount);
                Destroy(gameObject);
            }
        }
    }
}