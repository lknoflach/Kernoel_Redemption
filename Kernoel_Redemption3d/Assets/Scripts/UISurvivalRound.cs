using UnityEngine;

public class UISurvivalRound : MonoBehaviour
{
    //The canvas for the Menu
    public Canvas guiMenu;
    public Canvas guiUpgrade;

    //For managing the GUI
    private ManageSurvivalRounds _manageSurvivalRounds;

    private void Start()
    {
        _manageSurvivalRounds = GetComponent<ManageSurvivalRounds>();
        guiUpgrade.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_manageSurvivalRounds.showGuiSurvivalRounds)
        {
            guiMenu.gameObject.SetActive(true);
        }
        else
        {
            guiMenu.gameObject.SetActive(false);
        }
    }

    public void ShowGuiMenu()
    {
        _manageSurvivalRounds.showGuiUpgrade = false;
        _manageSurvivalRounds.showGuiSurvivalRounds = true;
    }

    public void ShowGuiUpgrade()
    {
        _manageSurvivalRounds.showGuiSurvivalRounds = false;
        HideGuiMenu();
        _manageSurvivalRounds.showGuiUpgrade = true;
    }

    public void HideGuiMenu()
    {
        _manageSurvivalRounds.showGuiSurvivalRounds = false;
        guiMenu.gameObject.SetActive(false);
    }

    public void ContinueGameHandler()
    {
        HideGuiMenu();
        _manageSurvivalRounds.endOfRound = true;
        _manageSurvivalRounds.continueGame = true;
    }
}