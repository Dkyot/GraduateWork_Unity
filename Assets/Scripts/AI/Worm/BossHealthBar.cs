using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] private CharacterStats boss;
    private HeartsHealthSystem bHealth;
    
    [SerializeField] private Image healthBar;
    [SerializeField] private Image hbBackground;
    private float fill;
    private int maxHealth;

    private float timer;
    private float timeBeforeAppearance = 1.2f;
    private bool isAppeared;

    public UnityEvent OnFinish;

    public BWCounter bWCounter;

    private void Start() {
        timer = 0;
        fill = 1f;
        healthBar.gameObject.SetActive(false);
        hbBackground.gameObject.SetActive(false);

        bHealth = boss.GetHealthSystem();
        bHealth.OnDamaged += HeartsHealthSystem_OnDamaged;
        bHealth.OnDead += HeartsHealthSystem_OnDead;
        maxHealth = bHealth.GetCurrentHP();
    }

    private void Update() {
        if (!isAppeared) {
            timer += Time.deltaTime;
            if (timer >= timeBeforeAppearance) {
                healthBar.gameObject.SetActive(true);
                hbBackground.gameObject.SetActive(true);
            }
            return;
        }
    }

    #region Events methods
    private void HeartsHealthSystem_OnDamaged(object sender, EventArgs e) {
        fill = (float)bHealth.GetCurrentHP() / maxHealth;
        healthBar.fillAmount = fill;
    }

    private void HeartsHealthSystem_OnDead(object sender, EventArgs e) {
        bWCounter.wormIsDear = true;
        OnFinish?.Invoke();
        Destroy(gameObject, 0.5f);
    }
    #endregion
}
