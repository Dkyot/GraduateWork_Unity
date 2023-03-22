using UnityEngine;

[CreateAssetMenu(fileName = "Loot_", menuName = "Loot/DropLoot")]
public class LootSO : ScriptableObject
{
    public GameObject lootPrefab;
    public string lootName = "Trash";
    public int dropChance = 100;

    //public bool magnetic = false;
    public float dropForce = 100f;
}
