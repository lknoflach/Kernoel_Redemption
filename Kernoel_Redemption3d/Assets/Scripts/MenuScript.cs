using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Title screen script
/// </summary>
public class MenuScript : MonoBehaviour
{
    public string currentSceneName = "Level1";
    public string mainMenuSceneName = "MainMenu";

    private void Update()
    {
        // Button "m": Terminate the Game
        if (Input.GetKeyDown("m")) LoadMainMenu();
        // Button "r": Restart the Game
        if (Input.GetKeyDown("r")) RestartScene();
        // Button "t": Terminate the Game
        if (Input.GetKeyDown("t")) QuitGame();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(currentSceneName);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void RestartScene()
    {
        var activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}