using System;
using UnityEngine;
using UnityEngine.Events;

public class CombatManager : MonoBehaviour
{
    public AnimController animController;
    [SerializeField] private bool shoot;

    [SerializeField] private ShootProjectiles shootProjectiles;

    [SerializeField] private UnityEvent AbilityAction;

    public event EventHandler OnDash;
    
    public void AttackHandler() {
        animController?.weaponAnimator.SetTrigger("Attack");
        if (shoot) shootProjectiles.Shoot();
    }

    public void Dash() {
        if (OnDash != null) OnDash(this, EventArgs.Empty);
    }

    public void Ability() {
        AbilityAction?.Invoke();
    }
}
