using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitcher : MonoBehaviour
{
    public BWCounter bWCounter;
    private SavePlayerDataBetweenScenes dataManager;
    [SerializeField] GameObject loadScreen;
    
    private void Start() {
        dataManager = FindObjectOfType<SavePlayerDataBetweenScenes>();
    }

    public void SwitchScene() {
        if (dataManager != null)
            dataManager.SaveData();
        if (loadScreen != null)
            loadScreen.SetActive(true);
        
        if (bWCounter.wormIsDear)
            SceneManager.LoadScene("GhostsLevelScene");
        else
            SceneManager.LoadScene("FirstLevelScene");
    }
}
