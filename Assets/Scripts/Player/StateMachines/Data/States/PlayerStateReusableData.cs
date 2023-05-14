using UnityEngine;

public class PlayerStateReusableData
{
    public Vector2 movementInput { get; set;}
    public float speedModifier { get; set;} = 1f;

    public bool shouldUseAbility { get; set;}
    public bool shouldAttack { get; set;}
}
