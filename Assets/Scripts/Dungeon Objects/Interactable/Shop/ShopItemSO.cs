using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemSO", menuName = "ShopItem")]
public class ShopItemSO : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public int itemCost;

    public Sprite sprite;

    public bool isSold;

    public Command command;

    public void GetItem(PlayerSO data, Transform player, Equipment equipment) {
        command.Execute(data, player, equipment);
    }

}
