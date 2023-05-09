using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame() {
        SceneManager.LoadScene("StartLevelScene");
    }

    public void QuitGame() {
        Debug.Log("Quit");
        Application.Quit();
    }
}
