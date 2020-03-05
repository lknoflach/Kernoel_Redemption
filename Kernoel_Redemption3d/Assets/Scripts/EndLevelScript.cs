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
        if(PlayerScript.numberOfClones < 1){
            score += GameManager.Instance.KernoilScore;
        }
        else{
            score = (score + GameManager.Instance.KernoilScore) * ((float)PlayerScript.numberOfClones * 0.5f + 1.0f);
        }
        seedOilAmountText.text = "Kernöl: " + score;
    }


}