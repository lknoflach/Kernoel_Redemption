using UnityEngine;
using UnityEngine.UI;

public class EndLevelScript : MonoBehaviour
{
    public GameObject score;
    
    private void Start()
    {
        if (score) UpdateUI();
    }

    private void Update()
    {
        if (score) UpdateUI();
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
        // Calculate the Score
        if (!score) return;

        // Fix GameManager missing BUG
        var gameManager = GameManager.Instance;
        if (!gameManager) gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        var values = score.transform.GetChild(1);
        
        // Level Points
        var levelPoints = 10 * gameManager.finishedLevelAmount;
        var levelPointsValue = values.Find("LevelPointsValue");
        if (levelPointsValue) levelPointsValue.GetComponent<Text>().text = $"10P * {gameManager.finishedLevelAmount}";

        // Seed Oil
        var seedOil = 1 * gameManager.seedOilAmount;
        var seedOilValue = values.Find("SeedOilValue");
        if (seedOilValue) seedOilValue.GetComponent<Text>().text = $"{gameManager.seedOilAmount}P";

        // Clones
        var cloneMultiplier = 1 + gameManager.cloneAmount;
        var cloneMultiplierValue = values.Find("CloneMultiplierValue");
        if (cloneMultiplierValue) cloneMultiplierValue.GetComponent<Text>().text = $"1 + {gameManager.cloneAmount}";

        // End Score
        var endScore = (levelPoints + seedOil) * cloneMultiplier;
        var endScoreValue = values.Find("EndScoreValue");
        if (endScoreValue) endScoreValue.GetComponent<Text>().text = $"({levelPoints}P + {seedOil}P) * {cloneMultiplier} = {endScore}P";
    }
}