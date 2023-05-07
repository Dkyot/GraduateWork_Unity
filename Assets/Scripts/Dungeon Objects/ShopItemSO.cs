using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemSO", menuName = "ShopItem")]
public class ShopItemSO : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public int itemCost;

    public bool isHeart;

    public void GetItem(PlayerSO data) {
        data.useMagneticField = true;
    }

}
