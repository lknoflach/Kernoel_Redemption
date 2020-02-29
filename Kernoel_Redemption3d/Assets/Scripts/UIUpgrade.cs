using UnityEngine;
using UnityEngine.UI;

public class UIUpgrade : MonoBehaviour
{

    //Canvas for the UI
    public Canvas guiUpgrade;
    //For for controlling the UI
    public GameObject survivalRoundManager;
    private ManageSurvivalRounds manageSurvivalRounds;
    //For upgrading the damage;
    public GameObject gun;
    private GunFiring gunFiring;
    public int totalWeaponDamage = 3;
    public float totalprojectileSpeedOfWeapon = 45;
    //the player components to set the player stats
    public GameObject player;   



    private CharacterMovement characterMovement;
    public float totalSpeed = 12f;
    private CloningScript cloningScript;

    // Bar for UI
    [Header("Speedbar")] public Image speedBar;
    [Header("DamageBar")] public Image damageBar;
    [Header("ProjectileSpeedBar")] public Image projectileSpeedBar;
    


    private void Start()
    {
        guiUpgrade.gameObject.SetActive(false);
        manageSurvivalRounds = GetComponent<ManageSurvivalRounds>();
        characterMovement = player.GetComponent<CharacterMovement>();
        cloningScript = player.GetComponent<CloningScript>();
        gunFiring = gun.GetComponent<GunFiring>();

        if (speedBar) speedBar.fillAmount = ((float) characterMovement.speed) / ((float) totalSpeed);
        if (damageBar) damageBar.fillAmount = ((float) gunFiring.damageOfWeapon) / ((float) totalWeaponDamage);
        if (projectileSpeedBar) projectileSpeedBar.fillAmount = ((float) gunFiring.projectileSpeedOfWeapon) / ((float) totalprojectileSpeedOfWeapon);
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
    public void UpgradeDamge(){
        
        if (gunFiring.damageOfWeapon < totalWeaponDamage && cloningScript.lowGradeSeedOil >= 5)
        {
            cloningScript.lowGradeSeedOil = cloningScript.lowGradeSeedOil - 3;
            gunFiring.damageOfWeapon = gunFiring.damageOfWeapon + 1; 
            if (damageBar) damageBar.fillAmount = ((float) gunFiring.damageOfWeapon) / ((float) totalWeaponDamage);
        }
    }

    public void UpgradeProjectileSpeed(){
        if (gunFiring.projectileSpeedOfWeapon < totalprojectileSpeedOfWeapon && cloningScript.lowGradeSeedOil >= 2)
        {
            cloningScript.lowGradeSeedOil = cloningScript.lowGradeSeedOil - 3;
            gunFiring.projectileSpeedOfWeapon = gunFiring.projectileSpeedOfWeapon + 5; 
            if (projectileSpeedBar) projectileSpeedBar.fillAmount = ((float) gunFiring.projectileSpeedOfWeapon) / ((float) totalprojectileSpeedOfWeapon);
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