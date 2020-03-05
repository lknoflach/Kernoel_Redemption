using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class EndLevelScript : MonoBehaviour
{
    [FormerlySerializedAs("kernölAmountText")] public Text seedOilAmountText;
    
    private void Start()
    {
        if (seedOilAmountText) UpdateUI();
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

    public void UpdateUI()
    {
        var score = 10.0f;
        if(GameManager.Instance.cloneAmount < 1){
            score += GameManager.Instance.seedOilAmount;
        }
        else{
            score = (score + GameManager.Instance.seedOilAmount) * (GameManager.Instance.cloneAmount * 0.5f + 1.0f);
        }
        seedOilAmountText.text = "Kernöl: " + score;
    }


}