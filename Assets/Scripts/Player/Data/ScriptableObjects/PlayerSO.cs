using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Characters/Player")]
public class PlayerSO : ScriptableObject
{
    [field: SerializeField] public PlayerGroundedData groundedData { get; private set;}

    // [SerializeField]
    // private PlayerGroundedData groundedData;

    // public PlayerGroundedData GroundedData { 
    //     get {return groundedData;}
    //     private set {groundedData = value;}
    // }
}
