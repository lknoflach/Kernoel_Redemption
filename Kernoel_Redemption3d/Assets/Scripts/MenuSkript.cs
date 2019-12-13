using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Title screen script
/// </summary>
public class MenuSkript : MonoBehaviour
{
    public void StartGame()
    {
        // "Stage1" is the name of the first scene we created.
        SceneManager.LoadScene("StefanKLevel", LoadSceneMode.Additive);

    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
}
