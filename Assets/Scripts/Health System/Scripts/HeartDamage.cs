using UnityEngine;

public class HeartDamage : MonoBehaviour
{
    [SerializeField] private int healAmount;

    private void OnTriggerEnter2D(Collider2D collider) {
        CharacterStats player = collider.GetComponent<CharacterStats>();
         if (player != null) {
            //if (healAmount >= 0)
                player.GetHealthSystem().Damage(healAmount);
            // else
            //     player.GetHealthSystem().Damage(healAmount);
            // //Destroy(gameObject);
        }
    }

}