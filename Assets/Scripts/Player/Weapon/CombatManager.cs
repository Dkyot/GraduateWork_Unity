using System;
using UnityEngine;
using UnityEngine.Events;

public class CombatManager : MonoBehaviour
{
    public AnimController animController;

    [SerializeField]
    private ShootProjectiles shootProjectiles;
    
    public void AttackHandler() {
        animController?.weaponAnimator.SetTrigger("Attack");
        shootProjectiles.Shoot();
    }



    public event EventHandler OnDash;

    public void Dash() {
        if (OnDash != null) OnDash(this, EventArgs.Empty);
    }

    //public event EventHandler OnAbility;
    [SerializeField]
    private UnityEvent AbilityAction;

    public void Ability() {
        //Debug.Log("нажатие");
        AbilityAction?.Invoke();
        //if (OnAbility != null) OnAbility(this, EventArgs.Empty);
    }
}
