using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndLevelScript : MonoBehaviour
{
    private GameManager _gameManager;
    public Text kernölAmountText;
    private void Start()
    {
        _gameManager = GameManager.Instance;
        kernölAmountText.text = "Kernöl: " + GameManager.Instance.KernoilScore;
        updateUI();
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

    public void updateUI()
    {
        kernölAmountText.text = "Kernöl: " + GameManager.Instance.KernoilScore;
    }


}