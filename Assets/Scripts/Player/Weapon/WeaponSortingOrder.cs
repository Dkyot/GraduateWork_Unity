using UnityEngine;

public class WeaponSortingOrder : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer characterRenderer;
    [SerializeField]
    private SpriteRenderer weaponRenderer;

    void Update()
    {
        if (transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180) {
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder - 1;
        } 
        else {
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder + 1;
        }
    }
}
