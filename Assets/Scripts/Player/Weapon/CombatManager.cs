using System;
using UnityEngine;

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
}
