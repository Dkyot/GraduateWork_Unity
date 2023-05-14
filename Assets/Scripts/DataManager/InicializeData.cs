using UnityEngine;

public class InicializeData : MonoBehaviour
{
    private void Awake() {
        SavePlayerDataBetweenScenes manager = GetComponent<SavePlayerDataBetweenScenes>();
        manager.InitializeData();
    }
}
