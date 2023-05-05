using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] int sceneNum;
    public void SwitchScene() {
        SceneManager.LoadScene(sceneNum);
        
    }
}
