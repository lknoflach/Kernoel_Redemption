using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Title screen script
/// </summary>
public class MenuScript : MonoBehaviour
{
    private void Update()
    {
        // Button "r": Restart the Game
        if (Input.GetKeyDown("r")) StartGame();
        // Button "t": Terminate the Game
        if (Input.GetKeyDown("t")) QuitGame();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("StefanKLevel");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
}