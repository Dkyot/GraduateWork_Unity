using UnityEngine;

public class ChangeShopItemSprite : MonoBehaviour
{
    [SerializeField] private SpriteRenderer itemSprite;
    [SerializeField] private BuyItem shop;

    [SerializeField] private Sprite defaultSprite;

    public void ChangeSprite() {
        if (shop.item == null) return;
        if (shop.item.sprite == null) return;
        itemSprite.sprite = shop.item.sprite;
    }

    public void Sold() {
        itemSprite.sprite = defaultSprite;
    }
}
