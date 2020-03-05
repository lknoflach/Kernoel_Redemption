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
            _gunFiring = _player.GetComponentInChildren<GunFiring>();
        }
        
        guiUpgrade.gameObject.SetActive(false);
        _manageSurvivalRounds = GetComponent<ManageSurvivalRounds>();

        if (speedBar) speedBar.fillAmount = _characterMovement.speed / totalSpeed;
        if (damageBar) damageBar.fillAmount = (float) _gunFiring.damageOfWeapon / totalWeaponDamage;
        if (projectileSpeedBar)
            projectileSpeedBar.fillAmount =
                _gunFiring.projectileSpeedOfWeapon / totalProjectileSpeedOfWeapon;
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
        if (_characterMovement.speed < totalSpeed && GameManager.Instance.seedOilAmount >= 3)
        {
            GameManager.Instance.UpdateSeedOilAmount(-3);
            
            _characterMovement.speed += 1f;
            if (speedBar) speedBar.fillAmount = _characterMovement.speed / totalSpeed;
        }
    }

    public void UpgradeDamage()
    {
        if (_gunFiring.damageOfWeapon < totalWeaponDamage && GameManager.Instance.seedOilAmount >= 5)
        {
            GameManager.Instance.UpdateSeedOilAmount(-3);
            
            _gunFiring.damageOfWeapon += 1;
            if (damageBar) damageBar.fillAmount = (float) _gunFiring.damageOfWeapon / totalWeaponDamage;

        }
    }

    public void UpgradeProjectileSpeed()
    {
        if (_gunFiring.projectileSpeedOfWeapon < totalProjectileSpeedOfWeapon && GameManager.Instance.seedOilAmount >= 2)
        {
            GameManager.Instance.UpdateSeedOilAmount(-3);
            
            _gunFiring.projectileSpeedOfWeapon += 5;
            if (projectileSpeedBar)
                projectileSpeedBar.fillAmount = _gunFiring.projectileSpeedOfWeapon / totalProjectileSpeedOfWeapon;
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