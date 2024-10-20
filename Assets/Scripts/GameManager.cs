using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Reload the current active scene
    public void ReloadScene()
    {
        // Get the currently active scene and reload it
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Quit the application
    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    public void LoadNextScene()
    {
        // Get the currently active scene and reload it
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
