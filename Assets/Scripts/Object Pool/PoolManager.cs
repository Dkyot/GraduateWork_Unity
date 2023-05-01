using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private Dictionary<string, Pool> pools = new Dictionary<string, Pool>();
    
    public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation) {
        Initialization(prefab);
        return pools[prefab.name].CreateObject(position, rotation);
    }

    public void Preload(GameObject prefab, int amount) {
        Initialization(prefab);
        GameObject[] objects = new GameObject[amount];

        for (int o = 0; o < amount; o++) {
            objects[o] = Spawn(prefab, Vector3.zero, Quaternion.identity);
        }

        for (int o = 0; o < amount; o++) {
            Despawn(objects[o]);
        }
    }

    public void Despawn(GameObject obj) {
        if (pools.ContainsKey(obj.name))
            pools[obj.name].ReturnObject(obj);
        else
            Destroy(obj);
    }

    private void Initialization(GameObject prefab) {
        if (prefab != null && pools.ContainsKey(prefab.name) == false)
            pools[prefab.name] = new Pool(prefab);
    }
}