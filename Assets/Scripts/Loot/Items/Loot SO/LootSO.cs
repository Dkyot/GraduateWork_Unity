using UnityEngine;

[CreateAssetMenu(fileName = "Loot_", menuName = "Loot/DropLoot")]
public class LootSO : ScriptableObject
{
    public GameObject lootPrefab;
    public string lootName = "Trash";
    public int dropChance = 100;
    public int minAmountOfLoot = 1;
    public int maxAmountOfLoot = 1;
    public float dropForce = 100f;
}
