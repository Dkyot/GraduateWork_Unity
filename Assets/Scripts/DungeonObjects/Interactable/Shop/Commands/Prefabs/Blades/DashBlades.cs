using System;
using UnityEngine;

public class DashBlades : MonoBehaviour
{
    private CombatManager combatManager;
    private MeleeAttackDetection attack;

    private void Start() {
        attack = GetComponent<MeleeAttackDetection>();
        combatManager = GetComponentInParent<CombatManager>();
        if (combatManager != null)
            combatManager.OnDash += OnBladeDash;
    }

    private void OnBladeDash(object sender, EventArgs e) {
        attack.DetectColliders();
    }
}
