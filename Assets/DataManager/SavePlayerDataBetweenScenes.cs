using UnityEngine;

public class SavePlayerDataBetweenScenes : MonoBehaviour
{
    public PlayerSO defaultData;
    public PlayerSO savedData;
    public PlayerSO currentData;
    public CharacterStats playerStats;
    public CoinStorage playerCoins;
    public Equipment equipment;
    
    private void Awake() {

    }

    public void InitializeData() {
        //Debug.Log("InitializeData");
        currentData.maxHeartsAmount = defaultData.maxHeartsAmount;
        currentData.currentHP = defaultData.currentHP;
        currentData.coins = defaultData.coins;
            currentData.useMagneticField = defaultData.useMagneticField;

        savedData.maxHeartsAmount = defaultData.maxHeartsAmount;
        savedData.currentHP = defaultData.currentHP;
        savedData.coins = defaultData.coins;
            savedData.useMagneticField = defaultData.useMagneticField; 
    }

    public void SaveData() {
        //Debug.Log("SaveData");
        int maxH = playerStats.GetHealthSystem().GetHeartList().Count;
        int currHP = playerStats.GetHealthSystem().GetCurrentHP();
        savedData.maxHeartsAmount = maxH;
        savedData.currentHP = currHP;

        int coins = playerCoins.GetCoinAmount();
        savedData.coins = coins; 

            savedData.useMagneticField = currentData.useMagneticField;
    }

    public void LoadData() {
        //Debug.Log("LoadData");
        currentData.maxHeartsAmount = savedData.maxHeartsAmount;
        currentData.currentHP = savedData.currentHP;
        currentData.coins = savedData.coins;
            
            currentData.useMagneticField = savedData.useMagneticField;
            equipment.UpdateData();
    }
}
