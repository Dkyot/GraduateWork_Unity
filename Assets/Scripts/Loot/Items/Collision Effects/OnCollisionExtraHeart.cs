using UnityEngine;

public class OnCollisionExtraHeart : LootBase
{
    private void OnTriggerEnter2D (Collider2D collider) {
        if (collider.gameObject.CompareTag("Player")) {
            CharacterStats player = collider.gameObject.GetComponent<CharacterStats>();
            if (player != null) {
                HeartsHealthSystem health =  player.GetHealthSystem();
                int currentHP = health.GetCurrentHP();
                health.AddHeart();
                health.SetHP(currentHP);
                //Debug.Log("++");
                Destroy(gameObject);
            }
        }
    }
}
