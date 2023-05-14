using UnityEngine;
using UnityEngine.Events;

public class OnDeathDrop : MonoBehaviour
{
    private CharacterStats characterStats;
    private HeartsHealthSystem heartsHealthSystem;

    [SerializeField] private UnityEvent OnDeath;

    private void Start() {
        characterStats = GetComponent<CharacterStats>();
        heartsHealthSystem = characterStats.GetHealthSystem();
        if (heartsHealthSystem != null) 
            heartsHealthSystem.OnDead += HeartsHealthSystem_OnDead;
    }

    private void HeartsHealthSystem_OnDead(object sender, System.EventArgs e) {
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
}
