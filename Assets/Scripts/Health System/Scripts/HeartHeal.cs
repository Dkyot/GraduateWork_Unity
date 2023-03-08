using UnityEngine;

public class HeartHeal : MonoBehaviour
{
    [SerializeField] private int healAmount;

    private void OnTriggerEnter2D(Collider2D collider) {
        Player player = collider.GetComponent<Player>();
        if (player != null) {
            //player.Heal(healAmount);
            Destroy(gameObject);
        }
    }

}