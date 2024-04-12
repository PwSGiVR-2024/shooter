using UnityEngine.SceneManagement;
using UnityEngine;
using StarterAssets;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    public GameObject DeathMenuUI;
    public FirstPersonController FirstPersonController;
    private bool canTogglePause = true;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (canTogglePause && !DeathMenuUI.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        FirstPersonController.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        FirstPersonController.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        //GameManager.instance.ResetScore();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetPause()
    {
        Time.timeScale = 1f;
        PauseMenuUI.SetActive(false);
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        canTogglePause = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        DeathMenuUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        canTogglePause = true;
    }
}
