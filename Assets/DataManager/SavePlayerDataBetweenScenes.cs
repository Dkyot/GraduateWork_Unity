using UnityEngine;

public class SavePlayerDataBetweenScenes : MonoBehaviour
{
    public PlayerSO defaultData;
    public PlayerSO savedData;
    public PlayerSO currentData;
    public CharacterStats playerStats;
    
    private void Awake() {

    }

    public void InitializeData() {
        //Debug.Log("InitializeData");
        currentData.maxHeartsAmount = defaultData.maxHeartsAmount;
        currentData.currentHP = defaultData.currentHP;
        savedData.maxHeartsAmount = defaultData.maxHeartsAmount;
        savedData.currentHP = defaultData.currentHP;
        
    }

    public void SaveData() {
        if (playerStats != null) {
            //Debug.Log("SaveData");
            int maxH = playerStats.GetHealthSystem().GetHeartList().Count;
            int currHP = playerStats.GetHealthSystem().GetCurrentHP();
            savedData.maxHeartsAmount = maxH;
            savedData.currentHP = currHP;
            return;
        }
        savedData.maxHeartsAmount = currentData.maxHeartsAmount;
        savedData.currentHP = currentData.currentHP;
    }

    public void LoadData() {
        //Debug.Log("LoadData");
        currentData.maxHeartsAmount = savedData.maxHeartsAmount;
        currentData.currentHP = savedData.currentHP;
    }
}
