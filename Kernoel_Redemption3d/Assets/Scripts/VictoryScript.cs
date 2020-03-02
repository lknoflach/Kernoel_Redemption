using UnityEngine;

public class VictoryScript : MonoBehaviour
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
        _gameManager.RestartScene();
    }

    public void QuitGame()
    {
        _gameManager.QuitGame();
    }
}