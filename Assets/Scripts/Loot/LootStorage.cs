using System.Collections.Generic;
using UnityEngine;

public class LootStorage : MonoBehaviour
{
    public List<LootSO> lootList = new List<LootSO>();

    private List<LootSO> GetDroppedItems() {
        int randomNumber = Random.Range(1, 101);
        List<LootSO> possibleIteams = new List<LootSO>();
        foreach (LootSO item in lootList) {
            if (randomNumber <= item.dropChance) {
                possibleIteams.Add(item);
                // добавить нужное количество
            }  
        }
        return possibleIteams;
    }

    public void InstantiateLoot() {
        List<LootSO> dropList = GetDroppedItems();
        if (dropList.Count == 0) return;

        foreach (LootSO item in dropList) {
            GameObject lootObject = Instantiate(item.lootPrefab, transform.position, Quaternion.identity);

            Vector2 dropDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            // выбрасывание
            lootObject.GetComponent<Rigidbody2D>()?.AddForce(dropDirection * item.dropForce, ForceMode2D.Impulse);
        }
    }
}
