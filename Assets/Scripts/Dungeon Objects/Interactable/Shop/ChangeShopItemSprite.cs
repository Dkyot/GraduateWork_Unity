using UnityEngine;

public class ChangeShopItemSprite : MonoBehaviour
{
    [SerializeField] private SpriteRenderer itemSprite;
    [SerializeField] private BuyItem shop;

    [SerializeField] private Sprite defaultSprite;

    public void ChangeSprite() {
        if (shop.item == null) {
            Debug.Log("не");
            return;
        }
        if (shop.item.sprite == null) {
            Debug.Log("qw");
            return;
        }
        itemSprite.sprite = shop.item.sprite;
    }

    public void Sold() {
        itemSprite.sprite = defaultSprite;
        Debug.Log("wwwwwww");
    }
}
