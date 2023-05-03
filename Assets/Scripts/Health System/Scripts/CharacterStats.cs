using System.Collections;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private int healthPoints = 5;

    private HeartsHealthSystem heartsHealth;

    private void Awake() {
        heartsHealth = new HeartsHealthSystem(healthPoints);
    }

    private void Start() {
        //heartsHealth.SetHP(1);
        //StartCoroutine(Regeneration(8, 1));
        //StartCoroutine(PeriodicDamage(8, 1));
    }
    
    private void Update() {
        //Debug.Log(heartsHealth.GetCurrentHP());  
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
