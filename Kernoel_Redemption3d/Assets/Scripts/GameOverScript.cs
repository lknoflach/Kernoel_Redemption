using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverScript : MonoBehaviour
{

    public bool isREStart;
    public bool isQuit;
    public bool isMenu;
    public PlayerHealthScript playerScene;
    string PrevScene;

    public void Start()
    {
        playerScene = GetComponent<PlayerHealthScript>();
        PrevScene = playerScene.PrevScene;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(PrevScene);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
 
}
