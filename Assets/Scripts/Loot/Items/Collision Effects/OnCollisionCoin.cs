using UnityEngine;

public class OnCollisionCoin : LootBase
{
    [SerializeField] 
    protected int coinAmount;
    
    private void OnCollisionEnter2D(Collision2D collider) {
        if (collider.gameObject.CompareTag("Player")) {
            CharacterStats player = collider.gameObject.GetComponent<CharacterStats>();
            if (player != null) {
                //player.GetHealthSystem().Heal(coinAmount);
                Destroy(gameObject);
            }
        }
    }
}
