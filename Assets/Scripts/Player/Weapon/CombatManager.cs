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
}
