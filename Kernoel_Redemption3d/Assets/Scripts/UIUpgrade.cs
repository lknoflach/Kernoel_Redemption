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
    private GunFiring _gunFiring;
    public int totalWeaponDamage = 3;

    [FormerlySerializedAs("totalprojectileSpeedOfWeapon")] public float totalProjectileSpeedOfWeapon = 45;

    // the player components to set the player stats
    private GameObject _player;

    private CharacterMovement _characterMovement;
    public float totalSpeed = 12f;
    private CloningScript _cloningScript;

    // Bar for UI
    [Header("SpeedBar")] public Image speedBar;
    [Header("DamageBar")] public Image damageBar;
    [Header("ProjectileSpeedBar")] public Image projectileSpeedBar;


    private void Start()
    {
        _player = GameObject.Find("PlayerHans");
        if (_player)
        {
            _characterMovement = _player.GetComponent<CharacterMovement>();
            _cloningScript = _player.GetComponent<CloningScript>();
            _gunFiring = _player.GetComponentInChildren<GunFiring>();
        }
        
        guiUpgrade.gameObject.SetActive(false);
        _manageSurvivalRounds = GetComponent<ManageSurvivalRounds>();

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
        if (_characterMovement.speed < totalSpeed && _cloningScript.highGradeSeedOil >= 3)
        {
            _cloningScript.highGradeSeedOil -= 3;
            _characterMovement.speed += 1f;
            if (speedBar) speedBar.fillAmount = ((float) _characterMovement.speed) / ((float) totalSpeed);
            _cloningScript.updateUI();
        }
    }

    public void UpgradeDamage()
    {
        if (_gunFiring.damageOfWeapon < totalWeaponDamage && _cloningScript.highGradeSeedOil >= 5)
        {
            _cloningScript.highGradeSeedOil -= 3;
            _gunFiring.damageOfWeapon += 1;
            if (damageBar) damageBar.fillAmount = ((float) _gunFiring.damageOfWeapon) / ((float) totalWeaponDamage);
            _cloningScript.updateUI();
        }
    }

    public void UpgradeProjectileSpeed()
    {
        if (_gunFiring.projectileSpeedOfWeapon < totalProjectileSpeedOfWeapon && _cloningScript.highGradeSeedOil >= 2)
        {
            _cloningScript.highGradeSeedOil -= 3;
            _gunFiring.projectileSpeedOfWeapon += 5;
            if (projectileSpeedBar)
                projectileSpeedBar.fillAmount = ((float) _gunFiring.projectileSpeedOfWeapon) /
                                                ((float) totalProjectileSpeedOfWeapon);
            _cloningScript.updateUI();
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