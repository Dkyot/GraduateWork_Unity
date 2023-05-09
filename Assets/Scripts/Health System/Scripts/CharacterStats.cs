using System.Collections;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private int healthPoints = 1;
    [SerializeField] private PlayerSO playerData = null;

    private HeartsHealthSystem heartsHealth;

    private void Start() {
        if (gameObject.tag == "Player") {
            heartsHealth = new HeartsHealthSystem(playerData.maxHeartsAmount);
            if (playerData.currentHP != 0)
                heartsHealth.SetHP(playerData.currentHP);
        }
        else
            heartsHealth = new HeartsHealthSystem(healthPoints);
    }

    private void Update() {
        // if (gameObject.tag != "Player")
        //     Debug.Log(heartsHealth.GetCurrentHP());
    }

    IEnumerator Regeneration(int hp, float timeInterval) {
        for (int i = 0; i < hp; i++) {
            heartsHealth.Heal(1);
            yield return new WaitForSeconds(timeInterval);
        }
    }

    IEnumerator PeriodicDamage(int hp, float timeInterval) {
        for (int i = 0; i < hp; i++) {
            heartsHealth.Damage(1);
            yield return new WaitForSeconds(timeInterval);
        }
    }

    public HeartsHealthSystem GetHealthSystem() {
        return heartsHealth;
    }
}
