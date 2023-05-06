using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Characters/Player")]
public class PlayerSO : ScriptableObject
{
    [field: SerializeField] public PlayerGroundedData groundedData { get; private set;}

    public int maxHeartsAmount;
    public int currentHP;

    public int coins;
}

