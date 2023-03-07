using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomType_", menuName = "RoomType")]
public class RoomTypeSO : ScriptableObject
{
    [Header("Room props data:")]
    public List<PropSO> roomProps;
}
