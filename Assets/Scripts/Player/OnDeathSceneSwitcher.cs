using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class OnDeathSceneSwitcher : MonoBehaviour
{
    private CharacterStats characterStats;
    private HeartsHealthSystem heartsHealthSystem;

    [SerializeField]
    private UnityEvent OnDeath;

    void Start() {
        Time.timeScale = 1;
        characterStats = GetComponent<CharacterStats>();
        heartsHealthSystem = characterStats.GetHealthSystem();
        if (heartsHealthSystem != null) {
            heartsHealthSystem.OnDead += HeartsHealthSystem_OnDead;
        }
    }

    private void HeartsHealthSystem_OnDead(object sender, System.EventArgs e) {
        OnDeath?.Invoke();
        //SceneManager.LoadScene("HubScene");
        Time.timeScale = 0;
    }
}
