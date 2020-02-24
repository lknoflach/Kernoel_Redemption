using UnityEngine;
using UnityEngine.UI;

public class UIUpgrade : MonoBehaviour
{

    //Canvas for the UI
    public Canvas guiUpgrade;
    //For for controlling the UI
    public GameObject survivalRoundManager;
    private ManageSurvivalRounds manageSurvivalRounds;

    //the player components to set the player stats
    public GameObject player;   



    private CharacterMovement characterMovement;
    public float totalSpeed = 12f;
    private CloningScript cloningScript;

    [Header("Unity Stuff")] public Image speedBar;

    private void Start()
    {
        guiUpgrade.gameObject.SetActive(false);
        manageSurvivalRounds = GetComponent<ManageSurvivalRounds>();
        characterMovement = player.GetComponent<CharacterMovement>();
        cloningScript = player.GetComponent<CloningScript>();
        if (speedBar) speedBar.fillAmount = ((float) characterMovement.speed) / ((float) totalSpeed);
    }

    private void Update() {
        if(manageSurvivalRounds.showGUIUpgrade){ 
            guiUpgrade.gameObject.SetActive(true);
        }else
        {
             guiUpgrade.gameObject.SetActive(false);
        }   
    }

    public void UpgradeSpeed()
    {
        if (characterMovement.speed < totalSpeed && cloningScript.lowGradeSeedOil >= 3)
        {
            cloningScript.lowGradeSeedOil = cloningScript.lowGradeSeedOil - 3;
            characterMovement.speed = characterMovement.speed + 1f;
            if (speedBar) speedBar.fillAmount = ((float) characterMovement.speed) / ((float) totalSpeed);
        }
    }

     public void BackToGuiMenu()
    {
        manageSurvivalRounds.showGUIUpgrade = false;
        guiUpgrade.gameObject.SetActive(false);
        manageSurvivalRounds.showGUISurvivalRounds = true;
        survivalRoundManager.gameObject.SetActive(true);
    }
}