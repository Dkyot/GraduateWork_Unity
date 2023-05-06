using UnityEngine;

public class LoadData : MonoBehaviour
{
    private void Awake() {
        SavePlayerDataBetweenScenes manager = GetComponent<SavePlayerDataBetweenScenes>();
        manager.LoadData();
    }
}
