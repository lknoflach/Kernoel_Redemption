using UnityEngine;


public class EndLevelScript : MonoBehaviour
{
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }
    
    public void LoadMainMenu()
    {
        _gameManager.LoadMainMenu();
    }

    public void RestartLevel()
    {
        _gameManager.RestartLevel();
    }

    public void QuitGame()
    {
        _gameManager.QuitGame();
    }
}