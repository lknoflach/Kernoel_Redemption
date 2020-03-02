using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Title screen script
/// </summary>
public class GameManager : MonoBehaviour
{
    public enum SceneNames
    {
        Campaign,
        GameOver,
        Endless,
        MainMenu,
        Prototype,
        Tutorial,
        Victory
    }
    public string currentSceneName = SceneNames.MainMenu.ToString();

    public static GameManager Instance { get; private set; }

    private readonly List<string> _availableSceneNames = new List<string>();

    private void Awake()
    {
        Debug.Log(SceneNames.GameOver.ToString());
        if (Instance != null && Instance != this)
        {
            // update the currentSceneName
            Instance.currentSceneName = currentSceneName;
            // destroy the duplicate
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        // Menus
        _availableSceneNames.Add(SceneNames.GameOver.ToString());
        _availableSceneNames.Add(SceneNames.MainMenu.ToString());
        _availableSceneNames.Add(SceneNames.Victory.ToString());
        // Levels
        _availableSceneNames.Add(SceneNames.Campaign.ToString());
        _availableSceneNames.Add(SceneNames.Endless.ToString());
        _availableSceneNames.Add(SceneNames.Prototype.ToString());
        _availableSceneNames.Add(SceneNames.Tutorial.ToString());
    }

    private void Update()
    {
        // Button "m": Load the Main Menu
        if (Input.GetKeyDown("m")) LoadMainMenu();
        // Button "r": Restart the Game
        if (Input.GetKeyDown("r")) RestartScene();
        // Button "t": Terminate the Game
        if (Input.GetKeyDown("t")) QuitGame();
    }

    public void LoadGameOverMenu()
    {
        SceneManager.LoadScene(SceneNames.GameOver.ToString());
    }
    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(SceneNames.MainMenu.ToString());
    }
    
    public void LoadVictoryMenu()
    {
        SceneManager.LoadScene(SceneNames.Victory.ToString());
    }

    public void RestartScene()
    {
        if (!_availableSceneNames.Contains(currentSceneName)) return;
        
        // var activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentSceneName);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void SetAndLoadCurrentScene(string sceneName)
    {
        if (!_availableSceneNames.Contains(sceneName)) return;
        
        currentSceneName = sceneName;
        SceneManager.LoadScene(currentSceneName);
    }
}