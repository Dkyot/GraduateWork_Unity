using UnityEngine;

public class HeartDamage : MonoBehaviour
{
    [SerializeField] private int healAmount;

    private void OnTriggerEnter2D(Collider2D collider) {
        CharacterHealth player = collider.GetComponent<CharacterHealth>();
         if (player != null) {
            //if (healAmount >= 0)
                player.GetHealthSystem().Damage(healAmount);
            // else
            //     player.GetHealthSystem().Damage(healAmount);
            // //Destroy(gameObject);
        }
    }

}