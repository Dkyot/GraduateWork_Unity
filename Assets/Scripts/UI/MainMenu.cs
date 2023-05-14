using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    #region Button methods
    public void PlayGame() {
        SceneManager.LoadScene("StartLevelScene");
    }

    public void QuitGame() {
        Debug.Log("Quit");
        Application.Quit();
    }
    #endregion
}
