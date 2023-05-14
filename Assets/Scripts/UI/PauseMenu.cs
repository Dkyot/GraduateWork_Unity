using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool paused;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject fpsCounter;
    [SerializeField] private GameObject coinCounter;
    [SerializeField] private GameObject health;
    [SerializeField] private GameObject deathScreen;

    [SerializeField] private GameObject bossHealthBar;

    [SerializeField] private PlayerInput playerInput;

    [SerializeField] private GameObject mobileInput;

    private bool death;

    public void Death() {
        death = true;

        deathScreen.SetActive(true);
        fpsCounter.SetActive(false);
        coinCounter.SetActive(false);
        health.SetActive(false);
        playerInput.inputActions.InGamePlayerInput.Disable();
        mobileInput.SetActive(false);

        if (bossHealthBar != null) bossHealthBar.SetActive(false);

        Time.timeScale = 0;
    }

    private void Start() {
        Play();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && !death) {
            if (paused) Play();
            else Stop();
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

        if (bossHealthBar != null) bossHealthBar.SetActive(false);

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
