using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool paused = false;
    public GameObject pauseMenu;
    public GameObject fpsCounter;
    public GameObject coinCounter;
    public GameObject health;

    public PlayerInput playerInput;

    public GameObject mobileInput;

    private bool deathScreen;

    public void Death() {
        deathScreen = true;
    }

    private void Start() {
        Play();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && !deathScreen) {
            if (paused)
                Play();
            else
                Stop();
        }
    }

    public void Play() {
        pauseMenu.SetActive(false);
        fpsCounter.SetActive(true);
        coinCounter.SetActive(true);
        health.SetActive(true);
        playerInput.inputActions.InGamePlayerInput.Enable();

            mobileInput.SetActive(Application.isMobilePlatform);

        Time.timeScale = 1;
        paused = false;
    }

    public void Stop() {
        pauseMenu.SetActive(true);
        fpsCounter.SetActive(false);
        coinCounter.SetActive(false);
        health.SetActive(false);
        playerInput.inputActions.InGamePlayerInput.Disable();

            mobileInput.SetActive(false);

        Time.timeScale = 0;
        paused = true;
    }

    public void ToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void ToHub() {
        SceneManager.LoadScene("HubScene");
    }
}
