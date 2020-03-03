using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    public void StartCampaignLevel()
    {
        var sceneName = GameManager.SceneNames.Campaign.ToString();
        _gameManager.SetAndLoadCurrentLevel(sceneName);
    }

    public void StartEndlessLevel()
    {
        var sceneName = GameManager.SceneNames.Endless.ToString();
        _gameManager.SetAndLoadCurrentLevel(sceneName);
    }

    public void StartTutorialLevel()
    {
        var sceneName = GameManager.SceneNames.Tutorial.ToString();
        _gameManager.SetAndLoadCurrentLevel(sceneName);
    }

    public void QuitGame()
    {
        _gameManager.QuitGame();
    }
}
