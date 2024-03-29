using System.Collections.Generic;
using UnityEngine;

public class SavePlayerDataBetweenScenes : MonoBehaviour
{
    public PlayerSO defaultData;
    public PlayerSO savedData;
    public PlayerSO currentData;
    public CharacterStats playerStats;
    public CoinStorage playerCoins;
    public Equipment equipment;

    public void InitializeData() {
        currentData.maxHeartsAmount = defaultData.maxHeartsAmount;
        currentData.currentHP = defaultData.currentHP;
        currentData.coins = defaultData.coins;

        savedData.maxHeartsAmount = defaultData.maxHeartsAmount;
        savedData.currentHP = defaultData.currentHP;
        savedData.coins = defaultData.coins;

        defaultData.commands = new List<Command>();
        currentData.commands = new List<Command>();
        savedData.commands = new List<Command>();
    }

    public void SaveData() {
        int maxH = playerStats.GetHealthSystem().GetHeartList().Count;
        int currHP = playerStats.GetHealthSystem().GetCurrentHP();
        savedData.maxHeartsAmount = maxH;
        savedData.currentHP = currHP;

        int coins = playerCoins.GetCoinAmount();
        savedData.coins = coins; 

        savedData.commands = new List<Command>(currentData.commands.Count);
        foreach (Command c in currentData.commands)
            savedData.commands.Add(c);
        currentData.commands = new List<Command>();
    }

    public void DeathSave() {
        int coins = playerCoins.GetCoinAmount();
        savedData.coins = coins / 2;
    }

    public void LoadData() {
        currentData.maxHeartsAmount = savedData.maxHeartsAmount;
        currentData.currentHP = savedData.currentHP;
        currentData.coins = savedData.coins;

        foreach (Command c in savedData.commands)
            currentData.commands.Add(c);
        equipment.UpdateData();
    }
}
