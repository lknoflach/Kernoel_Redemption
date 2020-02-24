using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISurvivalRound : MonoBehaviour
{

    //The canvas for the Menu
    public Canvas guiMenu;
    
    public Canvas guiUpgrade;

    //For managing the Gui
    public GameObject survivalRoundManager;
    private ManageSurvivalRounds manageSurvivalRounds;
    // Start is called before the first frame update

    
    void Start()
    {
        manageSurvivalRounds = GetComponent<ManageSurvivalRounds>();
        guiUpgrade.gameObject.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if(manageSurvivalRounds.showGUISurvivalRounds){
            guiMenu.gameObject.SetActive(true);
        }else
        {
            guiMenu.gameObject.SetActive(false);
        }
    }

    public void ShowGuiMenu(){
        
        manageSurvivalRounds.showGUIUpgrade = false;
        manageSurvivalRounds.showGUISurvivalRounds = true;
    }

    public void showGUIUpgrade(){
         manageSurvivalRounds.showGUISurvivalRounds = false;
        HideGuiMenu();
        manageSurvivalRounds.showGUIUpgrade = true;
    }
    
    public void HideGuiMenu(){
        manageSurvivalRounds.showGUISurvivalRounds = false;
        guiMenu.gameObject.SetActive(false);
    }

    public void ContinueGameHandler()
    {
        HideGuiMenu();
        manageSurvivalRounds.endOfRound = true;
        manageSurvivalRounds.continueGame = true;
    }

  
}
