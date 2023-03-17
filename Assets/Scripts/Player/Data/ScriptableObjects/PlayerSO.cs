using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Characters/Player")]
public class PlayerSO : ScriptableObject
{
    [field: SerializeField] public PlayerGroundedData groundedData { get; private set;}

    //[field: SerializeField] public Animator weaponAnimator;
    //[field: SerializeField] public Animation idle;
}
