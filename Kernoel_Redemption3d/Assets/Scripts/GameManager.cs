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
    
    public int seedOilAmount;
    public int cloneAmount;
    public int finishedLevelAmount;
    private readonly List<string> _finishedLevelNames = new List<string>();
    
    public static GameManager Instance { get; private set; }

    private readonly List<string> _availableSceneNames = new List<string>();
    public GameObject loadingScreen;
    public Slider loadingSlider;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            // update the currentSceneName
            if (!string.IsNullOrEmpty(currentSceneName)) Instance.currentSceneName = currentSceneName;
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
        
        _finishedLevelNames.Clear();
        seedOilAmount = 0;
        cloneAmount = 0;
        finishedLevelAmount = 0;
    }

    private void Update()
    {
        // Button "m": Load the Main Menu
        if (Input.GetKeyDown("m")) LoadMainMenu();
        
        // Button "r": Restart the Game
        // if (Input.GetKeyDown("r")) RestartLevel();
        
        // Button "Escape": Terminate the Game
        if (Input.GetKeyDown(KeyCode.Escape)) QuitGame();
    }

    public void LoadGameOverMenu()
    {
        // if (string.IsNullOrEmpty(currentSceneName))
        SceneManager.LoadScene(SceneNames.GameOver.ToString());
        
        seedOilAmount = 0;
        cloneAmount = 0;
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

        // Remove currentLevel if its already finished
        if (_finishedLevelNames.Contains(currentSceneName))
        {
            _finishedLevelNames.Remove(currentSceneName);
            finishedLevelAmount = _finishedLevelNames.Count;
        }
        
        if (loadingScreen) loadingScreen.gameObject.SetActive(true);
        StartCoroutine(LoadCurrentLevel());
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void UpdateSeedOilAmount(int amount)
    {
        seedOilAmount += amount;
        // Update text in UI
        var seedOilText = GameObject.Find("SeedOilAmount");
        if (seedOilText) seedOilText.GetComponent<Text>().text = $"Kernöl: {seedOilAmount}";
    }
    
    public void UpdateCloneAmount(int amount)
    {
        cloneAmount += amount;
        // Update text in UI
        var cloneAmountText = GameObject.Find("CloneAmount");
        if (cloneAmountText) cloneAmountText.GetComponent<Text>().text = $"Clones: {cloneAmount}";
    }
    
    public void SetAndLoadCurrentLevel(string sceneName)
    {
        if (!_availableSceneNames.Contains(sceneName)) return;
        
        currentSceneName = sceneName;
        if (loadingScreen) loadingScreen.gameObject.SetActive(true);
        StartCoroutine(LoadCurrentLevel());
    }

    public void FinishLevel(string nextLevelName)
    {
        var finishedLevel = SceneManager.GetActiveScene();
        if (!_finishedLevelNames.Contains(finishedLevel.name))
        {
            _finishedLevelNames.Add(finishedLevel.name);
            finishedLevelAmount = _finishedLevelNames.Count;
        }
        
        if (!string.IsNullOrEmpty(nextLevelName))
        {
            SetAndLoadCurrentLevel(nextLevelName);
        }
        else
        {
            LoadVictoryMenu();
        }
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