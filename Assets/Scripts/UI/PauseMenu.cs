using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject fpsCounter;
    [SerializeField] private GameObject coinCounter;
    [SerializeField] private GameObject health;
    [SerializeField] private GameObject deathScreen;

    [SerializeField] private GameObject bossHealthBar;

    [SerializeField] private PlayerInput playerInput;

    [SerializeField] private GameObject mobileInput;

    [SerializeField] private SettingsSO settings;

    private bool paused;
    private bool death;

    private void Start() {
        fpsCounter.SetActive(false);
        Play();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && !death) {
            if (paused) Play();
            else Stop();
        }
    }

    #region UI activation methods
    public void Play() {
        pauseMenu.SetActive(false);
        if (settings.showFPS)
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
    #endregion

    #region Button methods
    public void ToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void ToHub() {
        SceneManager.LoadScene("HubScene");
    }
    #endregion
}
