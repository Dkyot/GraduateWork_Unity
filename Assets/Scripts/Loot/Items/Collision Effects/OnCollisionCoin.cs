using UnityEngine;

public class OnCollisionCoin : LootBase
{
    [SerializeField] private int coinAmount;
    
    private void OnCollisionEnter2D(Collision2D collider) {
        if (collider.gameObject.CompareTag("Player")) {
            CoinStorage player = collider.gameObject.GetComponentInChildren<CoinStorage>();
            if (player != null) {
                player.AddCoins(coinAmount);
                Destroy(gameObject);
            }
        }
    }
}
