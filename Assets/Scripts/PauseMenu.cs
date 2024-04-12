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

    private bool canTogglePause = true; // Flag to control when pause menu can be toggled

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        // Check if the pause menu can be toggled
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

        // Enable camera rotation
        FirstPersonController.enabled = true;

        // Show and lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        // Disable camera rotation
        FirstPersonController.enabled = false;

        // Show and unlock cursor
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
        canTogglePause = true; // Reset the toggle flag
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        DeathMenuUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        canTogglePause = true; // Reset the toggle flag
    }

    // Method to enable pause menu toggle after a delay
    public void EnableTogglePause()
    {
        canTogglePause = true;
    }
}
