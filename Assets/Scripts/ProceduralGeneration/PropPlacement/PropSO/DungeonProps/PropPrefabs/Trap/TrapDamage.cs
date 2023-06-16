using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    [SerializeField] private int damageAmount;
    
    private void OnTriggerEnter2D (Collider2D collider) {
        if (collider.gameObject.CompareTag("Player")) {
            CharacterStats player = collider.gameObject.GetComponent<CharacterStats>();
            if (player != null) {
                //Debug.Log("dksjf");
                player.GetHealthSystem().Damage(damageAmount);
            }
            else Debug.Log("--");
        }
    }
}
