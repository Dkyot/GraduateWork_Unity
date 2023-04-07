using System;
using System.Collections.Generic;

public class HeartsHealthSystem
{
    public const int MAX_FRAGMENT_AMOUNT = 4;

    public event EventHandler OnDamaged;
    public event EventHandler OnHealed;
    public event EventHandler OnDead;

    private List<Heart> heartList;

    public HeartsHealthSystem(int heartAmount) {
        heartList = new List<Heart>();
        for (int i = 0; i < heartAmount; i++) {
            Heart heart = new Heart();
            heartList.Add(heart);
        }
    }

    public List<Heart> GetHeartList() {
        return heartList;
    }

    public int GetCurrentHP() {
        int currentHP = 0;
        foreach (Heart heart in heartList) {
            currentHP += heart.GetFragmentAmount();
        }
        return currentHP;
    }

    public void Damage(int damageAmount) {
        for (int i = heartList.Count - 1; i >= 0; i--) {
            Heart heart = heartList[i];
            if (damageAmount > heart.GetFragmentAmount()) {
                damageAmount -= heart.GetFragmentAmount();
                heart.Damage(heart.GetFragmentAmount());
            } 
            else {
                heart.Damage(damageAmount);
                break;
            }
        }

        if (OnDamaged != null) OnDamaged(this, EventArgs.Empty);

        if (IsDead()) {
            if (OnDead != null) OnDead(this, EventArgs.Empty);
        }
    }

    public void Heal(int healAmount) {
        for (int i = 0; i < heartList.Count; i++) {
            Heart heart = heartList[i];
            int missingFragments = MAX_FRAGMENT_AMOUNT - heart.GetFragmentAmount();
            if (healAmount > missingFragments) {
                healAmount -= missingFragments;
                heart.Heal(missingFragments);
            } 
            else {
                heart.Heal(healAmount);
                break;
            }
        }
        if (OnHealed != null) OnHealed(this, EventArgs.Empty);
    }

    public bool IsDead() {
        return GetCurrentHP() == 0;
    }
}