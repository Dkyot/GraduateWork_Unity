using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private int sceneNum;
    private SavePlayerDataBetweenScenes dataManager;

    private void Start() {
        dataManager = FindObjectOfType<SavePlayerDataBetweenScenes>();
    }

    public void SwitchScene() {
        if (dataManager != null)
            dataManager.SaveData();
        SceneManager.LoadScene(sceneNum);
    }
}
