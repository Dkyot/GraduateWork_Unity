using UnityEngine;

public class RestoreDataValues : MonoBehaviour
{
    private SavePlayerDataBetweenScenes manager;

    private void Awake() {
        manager = GetComponent<SavePlayerDataBetweenScenes>();
    }

    public void Restore() {
        HeartsHealthSystem health = manager.playerStats.GetHealthSystem();
        int hearts = health.GetHeartList().Count;
        health.SetHP(hearts * 4);
    }
}
