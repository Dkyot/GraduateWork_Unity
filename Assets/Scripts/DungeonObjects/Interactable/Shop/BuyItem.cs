using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class BuyItem : MonoBehaviour
{
    private Interactable interactable;

    public ShopAssortmentSO assortment;
    public ShopItemSO item = null;

    [SerializeField] private TextMeshProUGUI itemCost;
    [SerializeField] private TextMeshProUGUI itemName;

    [SerializeField] private UnityEvent OnChange;
    [SerializeField] private UnityEvent OnBuy;

    private void Start() {
        interactable = GetComponent<Interactable>();

        Change();
        OnChange?.Invoke();
    }

    private void Change() {
        foreach (ShopItemSO item in assortment.assortment) {
            if (item.isSold == false) {
                this.item = item;
                UpdateItemInfo();
                break;
            }
        }
    }

    private void UpdateItemInfo() {
        itemCost.text = item.itemCost.ToString();
        itemName.text = item.itemName.ToString();
    }

    public void Buy() {
        if (item == null) {
            //Debug.Log("a");
            return;
        }
        GameObject player = interactable.input.gameObject;
        CoinStorage storage = player.GetComponentInChildren<CoinStorage>();
        Equipment equipment = player.GetComponentInChildren<Equipment>();
        PlayerSO data = player.GetComponent<Player>().data;
        
        if (storage != null) {
            if (item.command.isActive) {
                //Debug.Log("b");
                return;
            }
            if (storage.SpendCoins(item.itemCost)) {
                OnBuy?.Invoke();
                item.GetItem(data, equipment.transform, equipment);
                item.isSold = true;
            }
        }
        else {
            //Debug.Log("c");
        }
    }
}
