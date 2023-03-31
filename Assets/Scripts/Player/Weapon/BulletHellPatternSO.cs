using UnityEngine;

[CreateAssetMenu(fileName = "bhPattern_", menuName = "BulletHellPattern")]
public class BulletHellPatternSO : ScriptableObject
{
    [Range(1, 36)]
    public int directionsAmount = 1;

    [Range(0.05f, 1f)]
    public float cooldown = 0.3f;

    public bool useBurst = false;
    [Range(0, 1f)]
    public float cooldownBetweenBursts = 0;

    public bool useRotation = false;
    public bool useSin = false;
    [Range(1f, 360f)]
    public float rotationSpeed = 5f;

    
}
