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
    
    private void Awake() {

    }

    public void InitializeData() {
        //Debug.Log("InitializeData");
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
        //Debug.Log("SaveData");
        int maxH = playerStats.GetHealthSystem().GetHeartList().Count;
        int currHP = playerStats.GetHealthSystem().GetCurrentHP();
        savedData.maxHeartsAmount = maxH;
        savedData.currentHP = currHP;

        int coins = playerCoins.GetCoinAmount();
        savedData.coins = coins; 

            //savedData.useMagneticField = currentData.useMagneticField;

            // savedData.commands = new List<Command>();
            // savedData.commands = currentData.commands;
            // currentData.commands = new List<Command>();

            // List<YourType> oldList = new List<YourType>();
            // List<YourType> newList = new List<YourType>(oldList.Count);

            // oldList.ForEach((item)=>{
            //     newList.Add(new YourType(item));
            // });

            //currentData.commands = new List<Command>();
            savedData.commands = new List<Command>(currentData.commands.Count);
            //Debug.Log(currentData.commands.Count);

            foreach (Command c in currentData.commands) {
                savedData.commands.Add(c);
            }
            currentData.commands = new List<Command>();
    }

    public void LoadData() {
        //Debug.Log("LoadData");
        currentData.maxHeartsAmount = savedData.maxHeartsAmount;
        currentData.currentHP = savedData.currentHP;
        currentData.coins = savedData.coins;

            //currentData.commands = new List<Command>();
            // currentData.commands = savedData.commands;
            // savedData.commands = new List<Command>();

            //equipment.UpdateData();
            foreach (Command c in savedData.commands) {
                    currentData.commands.Add(c);
            }

            equipment.UpdateData();
            
    }
}
