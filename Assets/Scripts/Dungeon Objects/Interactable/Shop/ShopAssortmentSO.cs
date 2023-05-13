using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopAssortmentSO", menuName = "ShopAssortment")]
public class ShopAssortmentSO : ScriptableObject
{
    public List<ShopItemSO> assortment;
}
