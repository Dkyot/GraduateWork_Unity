using UnityEngine;

public class OnCollisionExtraHeart : LootBase
{
    private void OnCollisionEnter2D(Collision2D collider) {
        if (collider.gameObject.CompareTag("Player")) {
            CharacterStats player = collider.gameObject.GetComponent<CharacterStats>();
            if (player != null) {
                HeartsHealthSystem health =  player.GetHealthSystem();
                int currentHP = health.GetCurrentHP();
                health.AddHeart();
                health.SetHP(currentHP);
                Destroy(gameObject);
            }
        }
    }
}
