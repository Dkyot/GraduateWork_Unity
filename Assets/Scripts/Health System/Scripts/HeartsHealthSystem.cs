using System;
using System.Collections.Generic;

public class HeartsHealthSystem
{
    public const int MAX_FRAGMENT_AMOUNT = 4;

    public event EventHandler OnDamaged;
    public event EventHandler OnHealed;
    public event EventHandler OnDead;
    public event EventHandler OnSet;

    public event EventHandler OnChangeHeartAmount;

    private List<Heart> heartList;

    public HeartsHealthSystem(int heartAmount) {
        heartList = new List<Heart>();
        for (int i = 0; i < heartAmount; i++) {
            Heart heart = new Heart();
            heartList.Add(heart);
        }
    }

    public void AddHeart() {
        Heart heart = new Heart();
        heartList.Add(heart);
        RefreshAllHearts();
        OnChangeHeartAmount(this, EventArgs.Empty);
    }

    public void RemoveHeart() {
        if (heartList.Count <= 1) return;
        Heart heart = new Heart();
        heartList.RemoveAt(heartList.Count - 1);
        RefreshAllHearts();
        OnChangeHeartAmount(this, EventArgs.Empty);
        
    }

    private void RefreshAllHearts() {
        int currHP = GetCurrentHP();
        IncreaseHP(heartList.Count * 4 - currHP);
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

    private void DecreaseHP(int amount) {
        for (int i = heartList.Count - 1; i >= 0; i--) {
            Heart heart = heartList[i];
            if (amount > heart.GetFragmentAmount()) {
                amount -= heart.GetFragmentAmount();
                heart.Damage(heart.GetFragmentAmount());
            } 
            else {
                heart.Damage(amount);
                break;
            }
        }
    }

    private void IncreaseHP(int amount) {
        for (int i = 0; i < heartList.Count; i++) {
            Heart heart = heartList[i];
            int missingFragments = MAX_FRAGMENT_AMOUNT - heart.GetFragmentAmount();
            if (amount > missingFragments) {
                amount -= missingFragments;
                heart.Heal(missingFragments);
            } 
            else {
                heart.Heal(amount);
                break;
            }
        }
    }

    public void SetHP(int hp) {
        if (hp > heartList.Count * 4 || hp <= 0) return;
        if (hp > GetCurrentHP()) IncreaseHP(hp - GetCurrentHP());
        else if (hp < GetCurrentHP()) DecreaseHP(GetCurrentHP() - hp);

        OnSet(this, EventArgs.Empty);
    } 

    public void Damage(int damageAmount) {
        if (damageAmount <= 0) return;
        DecreaseHP(damageAmount);
        if (OnDamaged != null) OnDamaged(this, EventArgs.Empty);
        if (IsDead()) {
            if (OnDead != null) OnDead(this, EventArgs.Empty);
        }
    }

    public void Heal(int healAmount) {
        IncreaseHP(healAmount);
        if (OnHealed != null) OnHealed(this, EventArgs.Empty);
    }

    public bool IsDead() {
        return GetCurrentHP() == 0;
    }
}