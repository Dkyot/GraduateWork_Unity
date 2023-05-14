using UnityEngine;

[CreateAssetMenu(fileName = "Prop_", menuName = "Prop")]
public class PropSO : ScriptableObject
{
    public GameObject propPrefab;

    [Space, Header("Placement type:")]
    public bool Corner = true;
    public bool NearWallUP = true;
    public bool NearWallDown = true;
    public bool NearWallRight = true;
    public bool NearWallLeft = true;
    public bool Inner = true;
    [Min(1)]
    public int PlacementQuantityMin = 1;
    [Min(1)]
    public int PlacementQuantityMax = 1;   
}