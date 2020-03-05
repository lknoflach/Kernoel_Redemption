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
        float score = 10.0f;
        if(GameManager.Instance.CloneAmount < 1.0f){
            score += GameManager.Instance.KernoilScore;
        }
        else{
            score = (score + GameManager.Instance.KernoilScore) * (GameManager.Instance.CloneAmount * 0.5f + 1);
        }
        seedOilAmountText.text = "Kernöl: " + score;
    }


}