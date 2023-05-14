using UnityEngine;
using UnityEngine.Events;

public class OnDeathSceneSwitcher : MonoBehaviour
{
    private CharacterStats characterStats;
    private HeartsHealthSystem heartsHealthSystem;

    [SerializeField]
    private UnityEvent OnDeath;

    private void Start() {
        Time.timeScale = 1;
        characterStats = GetComponent<CharacterStats>();
        heartsHealthSystem = characterStats.GetHealthSystem();
        if (heartsHealthSystem != null) {
            heartsHealthSystem.OnDead += HeartsHealthSystem_OnDead;
        }
    }

    private void HeartsHealthSystem_OnDead(object sender, System.EventArgs e) {
        OnDeath?.Invoke();
    }
}
