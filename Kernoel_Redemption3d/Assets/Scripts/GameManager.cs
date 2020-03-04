using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Title screen script
/// </summary>
public class GameManager : MonoBehaviour
{
    public enum SceneNames
    {
        Aufsteirern,
        Campaign,
        GameOver,
        Endless,
        MainMenu,
        Prototype,
        Tutorial,
        Victory
    }
    public string currentSceneName;

    public static GameManager Instance { get; private set; }

    private readonly List<string> _availableSceneNames = new List<string>();
    public GameObject loadingScreen;
    public Slider loadingSlider;

    private void Awake()
    {
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
        _availableSceneNames.Add(SceneNames.Aufsteirern.ToString());
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
        // if (Input.GetKeyDown("r")) RestartLevel();
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

    public void RestartLevel()
    {
        if (!_availableSceneNames.Contains(currentSceneName)) return;
        
        // var activeScene = SceneManager.GetActiveScene();
        // SceneManager.LoadScene(currentSceneName);
        StartCoroutine(LoadCurrentLevel());
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void SetAndLoadCurrentLevel(string sceneName)
    {
        if (!_availableSceneNames.Contains(sceneName)) return;
        
        currentSceneName = sceneName;
        if (loadingScreen) loadingScreen.gameObject.SetActive(true);
        StartCoroutine(LoadCurrentLevel());
    }
    
    // The coroutine runs on its own at the same time as Update() and takes an integer indicating which scene to load.
    private IEnumerator LoadCurrentLevel() 
    {
        // This line waits for 3 seconds before executing the next line in the coroutine.
        // This line is only necessary for this demo. The scenes are so simple that they load too fast to read the "Loading..." text.
        // yield return new WaitForSeconds(3);

        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        var asyncOperation = SceneManager.LoadSceneAsync(currentSceneName);

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!asyncOperation.isDone)
        {
            if (loadingSlider)
            {
                var progress = Mathf.Clamp01(asyncOperation.progress / .9f);
                loadingSlider.value = progress;
            }

            // async.progress;
            yield return null;
        }
        
        if (loadingScreen) loadingScreen.gameObject.SetActive(false);
    }
}