using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelMessage : MonoBehaviour
{
    // private bool _showName;
    public string nextLevelName;

    private void OnTriggerEnter(Collider other)
    {
        // sets display message to true an displays the message
        if (!other.gameObject.CompareTag("Player")) return;

        if (nextLevelName != null && nextLevelName != "")
        {
            GameManager.Instance.SetAndLoadCurrentLevel(nextLevelName);
        }
        else
        {
            // Set activeScene as currentScene before loading VictoryMenu
            var activeScene = SceneManager.GetActiveScene();
            GameManager.Instance.currentSceneName = activeScene.name;
            GameManager.Instance.LoadVictoryMenu();
            // _showName = true;
        }
    }

    /*private void OnGUI()
    {
        if (_showName)
            GUI.Label(new Rect(400, 200, 2000, 2000), "You finished the level! :)");
    }*/
}