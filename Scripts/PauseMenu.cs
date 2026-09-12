using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private MonoBehaviour playerMovementScript; // Reference to movement script
    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1;

        // Enable or disable movement script
        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = !isPaused;
        }
    }

    public void Resume()
    {
        TogglePause();
    }

    public void QuitGame()
    {
        Debug.Log("Spillet Avsluttes");
        Application.Quit();
    }
}