using UnityEngine;

public class HeartHeal : LootBase
{
    [SerializeField] 
    protected int healAmount;
    
    private void OnCollisionEnter2D(Collision2D collider) {
        CharacterStats player = collider.gameObject.GetComponent<CharacterStats>();
        if (player != null) {
            player.GetHealthSystem().Heal(healAmount);
            Destroy(gameObject);
        }
    }
}