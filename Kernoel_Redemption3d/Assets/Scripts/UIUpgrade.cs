using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIUpgrade : MonoBehaviour
{
    //Canvas for the UI
    public Canvas guiUpgrade;

    //For for controlling the UI
    public GameObject survivalRoundManager;

    private ManageSurvivalRounds _manageSurvivalRounds;

    //For upgrading the damage;
    public GameObject gun;
    private GunFiring _gunFiring;
    public int totalWeaponDamage = 3;

    [FormerlySerializedAs("totalprojectileSpeedOfWeapon")] public float totalProjectileSpeedOfWeapon = 45;

    //the player components to set the player stats
    public GameObject player;


    private CharacterMovement _characterMovement;
    public float totalSpeed = 12f;
    private CloningScript _cloningScript;

    // Bar for UI
    [Header("SpeedBar")] public Image speedBar;
    [Header("DamageBar")] public Image damageBar;
    [Header("ProjectileSpeedBar")] public Image projectileSpeedBar;


    private void Start()
    {
        guiUpgrade.gameObject.SetActive(false);
        _manageSurvivalRounds = GetComponent<ManageSurvivalRounds>();
        _characterMovement = player.GetComponent<CharacterMovement>();
        _cloningScript = player.GetComponent<CloningScript>();
        _gunFiring = gun.GetComponent<GunFiring>();

        if (speedBar) speedBar.fillAmount = ((float) _characterMovement.speed) / ((float) totalSpeed);
        if (damageBar) damageBar.fillAmount = ((float) _gunFiring.damageOfWeapon) / ((float) totalWeaponDamage);
        if (projectileSpeedBar)
            projectileSpeedBar.fillAmount =
                ((float) _gunFiring.projectileSpeedOfWeapon) / ((float) totalProjectileSpeedOfWeapon);
    }

    private void Update()
    {
        if (_manageSurvivalRounds.showGUIUpgrade)
        {
            guiUpgrade.gameObject.SetActive(true);
        }
        else
        {
            guiUpgrade.gameObject.SetActive(false);
        }
    }

    public void UpgradeSpeed()
    {
        if (_characterMovement.speed < totalSpeed && _cloningScript.lowGradeSeedOil >= 3)
        {
            _cloningScript.lowGradeSeedOil -= 3;
            _characterMovement.speed += 1f;
            if (speedBar) speedBar.fillAmount = ((float) _characterMovement.speed) / ((float) totalSpeed);
        }
    }

    public void UpgradeDamage()
    {
        if (_gunFiring.damageOfWeapon < totalWeaponDamage && _cloningScript.lowGradeSeedOil >= 5)
        {
            _cloningScript.lowGradeSeedOil -= 3;
            _gunFiring.damageOfWeapon += 1;
            if (damageBar) damageBar.fillAmount = ((float) _gunFiring.damageOfWeapon) / ((float) totalWeaponDamage);
        }
    }

    public void UpgradeProjectileSpeed()
    {
        if (_gunFiring.projectileSpeedOfWeapon < totalProjectileSpeedOfWeapon && _cloningScript.lowGradeSeedOil >= 2)
        {
            _cloningScript.lowGradeSeedOil -= 3;
            _gunFiring.projectileSpeedOfWeapon += 5;
            if (projectileSpeedBar)
                projectileSpeedBar.fillAmount = ((float) _gunFiring.projectileSpeedOfWeapon) /
                                                ((float) totalProjectileSpeedOfWeapon);
        }
    }

    public void BackToGuiMenu()
    {
        _manageSurvivalRounds.showGUIUpgrade = false;
        guiUpgrade.gameObject.SetActive(false);
        _manageSurvivalRounds.showGUISurvivalRounds = true;
        survivalRoundManager.gameObject.SetActive(true);
    }
}