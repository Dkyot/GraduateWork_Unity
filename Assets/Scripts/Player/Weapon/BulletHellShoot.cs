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

    private void Awake() {
        shootDirections = CalculateDiractions();
        cooldownTimer = 0;
        burstTimer = 0;
        inBurst = true;
    }

    private void Update() {
        cooldownTimer += Time.deltaTime;
        burstTimer += Time.deltaTime;

        Rotation();

        if (Cooldown() && Burst())
            foreach(Vector2 vector in shootDirections)
                Shoot(vector);
    }  

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

    private float GetAngleFromVectorFloat(Vector3 direction) {
        direction = direction.normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360;
        return angle;
    }
    
    private List<Vector2> CalculateDiractions() {
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

    public void Shoot(Vector2 vector) {
        GameObject bullet;
        if (poolManager == null)
            return;
        else 
            bullet = poolManager.Spawn(bulletPhysics, transform.position, Quaternion.identity);
        bullet.GetComponent<BulletPhysics>().Setup(poolManager, vector.normalized, gameObject.layer);
    }

    private void OnDrawGizmos() {
        if (!bulletHellData.useRotation)
            shootDirections = CalculateDiractions();
        Gizmos.color = Color.red;
        if (shootDirections != null)
            foreach (Vector2 vector in shootDirections)
                Gizmos.DrawRay(transform.position, vector * 2f);
    }
}
