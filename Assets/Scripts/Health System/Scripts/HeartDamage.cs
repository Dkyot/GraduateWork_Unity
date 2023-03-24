using UnityEngine;

public class HeartDamage : MonoBehaviour
{
    [SerializeField] private int healAmount;

    private void OnCollisionEnter2D(Collision2D collider) {
        CharacterStats player = collider.gameObject.GetComponent<CharacterStats>();
         if (player != null) {
            player.GetHealthSystem().Damage(healAmount);
            Destroy(gameObject);
        }
    }

}