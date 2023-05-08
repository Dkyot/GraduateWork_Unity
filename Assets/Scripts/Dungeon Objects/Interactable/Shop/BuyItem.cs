using UnityEngine;

public class BuyItem : MonoBehaviour
{
    private Interactable interactable;
    public ShopItemSO item;

    private void Start() {
        interactable = GetComponent<Interactable>();
    }

    public void Buy() {
        GameObject player = interactable.input.gameObject;
        CoinStorage storage = player.GetComponentInChildren<CoinStorage>();
        Equipment equipment = player.GetComponentInChildren<Equipment>();
        PlayerSO data = player.GetComponent<Player>().data;
        
        if (storage != null) {
            if (item.command.isActive) return;
            if (storage.SpendCoins(item.itemCost)) {
                item.GetItem(data, player.transform, equipment);
                //equipment.UpdateData();
                //Debug.Log("успешно");
            }
            else {
                //Debug.Log("нужно больше денег");
            }
        }
    }
}
