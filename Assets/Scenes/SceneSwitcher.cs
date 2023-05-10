using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private int sceneNum;
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
        SceneManager.LoadScene(sceneNum);
    }
}
