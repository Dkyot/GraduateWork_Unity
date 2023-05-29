using System.Collections.Generic;
using UnityEngine;

public class BulletHellShoot : MonoBehaviour
{
    [SerializeField] private BulletHellPatternSO bulletHellData;
    [SerializeField] private GameObject bulletPhysics;

    [SerializeField] private PoolManager poolManager;

    private List<Vector2> shootDirections;

    private float cooldownTimer;

    private float burstTimer;
    private bool inBurst;

    public bool shouldShoot = true;

    private void Awake() {
        shootDirections = CalculateDirections();
        cooldownTimer = 0;
        burstTimer = 0;
        inBurst = true;
    }

    private void Update() {
        cooldownTimer += Time.deltaTime;
        burstTimer += Time.deltaTime;

        Rotation();

        if (Cooldown() && Burst() && shouldShoot)
            foreach(Vector2 vector in shootDirections)
                Shoot(vector);
    }

    public void ShootOnce() {
        foreach(Vector2 vector in shootDirections)
            Shoot(vector);
    }

    #region Delay methods
    private bool Cooldown() {
        if (cooldownTimer >= bulletHellData.cooldown) {
            cooldownTimer = 0;
            return true;
        }
        return false;
    }

    private bool Burst() {
        if (!bulletHellData.useBurst) return true;
        if (burstTimer >= bulletHellData.cooldownBetweenBursts) {
            burstTimer = 0;
            inBurst = !inBurst;
        }
        return inBurst;
    }
    #endregion

    #region Auxiliary methods
    private void Rotation() {
        if (bulletHellData.useRotation) {
            for (int i = 0; i < shootDirections.Count; i++) {
                float angle = bulletHellData.useSin ? 
                    bulletHellData.rotationSpeed * Time.deltaTime *  Mathf.Sin(Time.time) : 
                    bulletHellData.rotationSpeed * Time.deltaTime;
                if (bulletHellData.clockwise) angle *= -1;
                shootDirections[i] = RotateVector(shootDirections[i], angle); 
            }
        }
    }
    
    private List<Vector2> CalculateDirections() {
        List<Vector2> directions = new List<Vector2>();
        
        float angle = 360f / bulletHellData.directionsAmount;

        Vector2 dir = Vector2.right;
        for (int i = 0; i < bulletHellData.directionsAmount; i++) {
            directions.Add(dir);
            dir = RotateVector(dir, angle);
        }
        return directions;
    }

    private Vector2 RotateVector(Vector2 vector, float angle) {
        return Quaternion.AngleAxis(angle, Vector3.forward) * vector;
    }

    private void Shoot(Vector2 vector) {
        GameObject bullet;
        if (poolManager == null)
           return;
        else 
           bullet = poolManager.Spawn(bulletPhysics, transform.position, Quaternion.identity);
                    //bullet = Instantiate(bulletPhysics, transform.position, Quaternion.identity);
        bullet.GetComponent<BulletPhysics>().Setup(poolManager, vector.normalized, gameObject.layer);
    }
    #endregion

    #region Debug methods
    private void OnDrawGizmos() {
        if (!bulletHellData.useRotation)
            shootDirections = CalculateDirections();
        Gizmos.color = Color.red;
        if (shootDirections != null)
            foreach (Vector2 vector in shootDirections)
                Gizmos.DrawRay(transform.position, vector * 2f);
    }
    #endregion
}
