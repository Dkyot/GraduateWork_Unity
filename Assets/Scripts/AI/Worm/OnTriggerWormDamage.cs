using UnityEngine;

public class OnTriggerWormDamage : MonoBehaviour
{
    [SerializeField] private int damageAmount;
    
    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("Player")) {
            CharacterStats player = collider.gameObject.GetComponent<CharacterStats>();
            if (player != null) {
                player.GetHealthSystem().Damage(damageAmount);
            }
        }
    }
}
