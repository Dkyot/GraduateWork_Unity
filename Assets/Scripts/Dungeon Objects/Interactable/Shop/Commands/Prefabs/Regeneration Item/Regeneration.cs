using UnityEngine;

public class Regeneration : MonoBehaviour
{
    private bool isSet;
    private HeartsHealthSystem health;
    private CharacterStats player;

    private float interval = 5f;
    private float timer;
    
    private void Start() {
        player = GetComponentInParent<CharacterStats>();
        timer = 0;
    }

    private void Update() {
        if (!isSet) {
            health = player.GetHealthSystem();
            if (health != null) isSet = true;
            return;
        }

        timer += Time.deltaTime;
        if (timer >= interval) {
            health.Heal(1);
            timer = 0;
        }
        
    }
}
