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
        if(_manageSurvivalRounds.showGUISurvivalRounds){
            guiMenu.gameObject.SetActive(true);
        }else
        {
            guiMenu.gameObject.SetActive(false);
        }
    }

    public void ShowGuiMenu(){
        
        _manageSurvivalRounds.showGUIUpgrade = false;
        _manageSurvivalRounds.showGUISurvivalRounds = true;
    }

    public void ShowGuiUpgrade(){
         _manageSurvivalRounds.showGUISurvivalRounds = false;
        HideGuiMenu();
        _manageSurvivalRounds.showGUIUpgrade = true;
    }
    
    public void HideGuiMenu(){
        _manageSurvivalRounds.showGUISurvivalRounds = false;
        guiMenu.gameObject.SetActive(false);
    }

    public void ContinueGameHandler()
    {
        HideGuiMenu();
        _manageSurvivalRounds.endOfRound = true;
        _manageSurvivalRounds.continueGame = true;
    }
}
