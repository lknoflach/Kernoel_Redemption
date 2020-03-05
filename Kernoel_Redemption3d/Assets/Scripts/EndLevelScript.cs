using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndLevelScript : MonoBehaviour
{
    public Text kernölAmountText;
    
    private void Start()
    {
        kernölAmountText.text = "Kernöl: " + GameManager.Instance.KernoilScore;
        updateUI();
    }
    
    public void LoadMainMenu()
    {
        GameManager.Instance.LoadMainMenu();
    }

    public void RestartLevel()
    {
        GameManager.Instance.RestartLevel();
    }

    public void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }

    public void updateUI()
    {
        kernölAmountText.text = "Kernöl: " + GameManager.Instance.KernoilScore;
    }


}