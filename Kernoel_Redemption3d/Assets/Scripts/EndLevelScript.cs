using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class EndLevelScript : MonoBehaviour
{
    [FormerlySerializedAs("kernölAmountText")] public Text seedOilAmountText;
    
    private void Start()
    {
        if (seedOilAmountText) updateUI();
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
        seedOilAmountText.text = "Kernöl: " + GameManager.Instance.KernoilScore;
    }


}