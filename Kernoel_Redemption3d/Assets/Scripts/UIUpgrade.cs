using UnityEngine;
using UnityEngine.UI;

public class UIUpgrade : MonoBehaviour
{
    //Canvas for the UI
    public Canvas guiUpgrade;

    //For for controlling the UI
    public GameObject survivalRoundManager;
    private ManageSurvivalRounds _manageSurvivalRounds;

    // the player components to set the player stats
    private GameObject _player;
    private CharacterMovement _characterMovement;
    private HealthScript _playerHealthScript;
    private GunFiring _gunFiring;

    // Bars for UI
    [Header("Speed")]
    public Image speedBar;
    public float speedMax = 12f;
    public int speedUpgradeCost = 3;
    public float speedUpgradeValue = 1f;
    
    [Header("Damage")] 
    public Image damageBar;
    public int damageMax = 3;
    public int damageUpgradeCost = 5;
    public int damageUpgradeValue = 1;

    [Header("ProjectileSpeed")] 
    public Image projectileSpeedBar;
    public float projectileSpeedMax = 45;
    public int projectileSpeedUpgradeCost = 5;
    public int playerHealthUpgradeValue = 10;
    
    [Header("PlayerHealth")] 
    public Image playerHealthBar;
    public int playerHealthMax = 100;
    public int playerHealthUpgradeCost = 10;
    public float projectileSpeedUpgradeValue = 5f;

    private void Start()
    {
        _player = GameObject.Find("PlayerHans");
        if (_player)
        {
            _characterMovement = _player.GetComponent<CharacterMovement>();
            _gunFiring = _player.GetComponentInChildren<GunFiring>();
            _playerHealthScript = _player.GetComponent<HealthScript>();

            playerHealthMax = _playerHealthScript.totalHealth;

            if (speedBar) speedBar.fillAmount = _characterMovement.speed / speedMax;
            if (damageBar) damageBar.fillAmount = (float) _gunFiring.damageOfWeapon / damageMax;
            if (projectileSpeedBar) projectileSpeedBar.fillAmount = _gunFiring.projectileSpeedOfWeapon / projectileSpeedMax;
            if (playerHealthBar) playerHealthBar.fillAmount = (float) _playerHealthScript.currentHealth / playerHealthMax;
        }
        
        guiUpgrade.gameObject.SetActive(false);
        _manageSurvivalRounds = GetComponent<ManageSurvivalRounds>();
    }

    private void Update()
    {
        if (_manageSurvivalRounds.showGuiUpgrade) if (playerHealthBar) playerHealthBar.fillAmount = (float) _playerHealthScript.currentHealth / playerHealthMax;
        guiUpgrade.gameObject.SetActive(_manageSurvivalRounds.showGuiUpgrade);
    }

    public void UpgradeSpeed()
    {
        if (_characterMovement.speed >= speedMax || GameManager.Instance.seedOilAmount < speedUpgradeCost) return;
        
        GameManager.Instance.UpdateSeedOilAmount(-speedUpgradeCost);
        _characterMovement.speed += speedUpgradeValue;
        
        if (_characterMovement.speed > speedMax) _characterMovement.speed = speedMax;
        if (speedBar) speedBar.fillAmount = _characterMovement.speed / speedMax;
    }

    public void UpgradeDamage()
    {
        if (_gunFiring.damageOfWeapon >= damageMax || GameManager.Instance.seedOilAmount < damageUpgradeCost) return;
        
        GameManager.Instance.UpdateSeedOilAmount(-damageUpgradeCost);
        _gunFiring.damageOfWeapon += damageUpgradeValue;
        
        if (_gunFiring.damageOfWeapon > damageMax) _gunFiring.damageOfWeapon = damageMax;
        if (damageBar) damageBar.fillAmount = (float) _gunFiring.damageOfWeapon / damageMax;
    }

    public void UpgradeProjectileSpeed()
    {
        if (_gunFiring.projectileSpeedOfWeapon >= projectileSpeedMax || GameManager.Instance.seedOilAmount < projectileSpeedUpgradeCost) return;
       
        GameManager.Instance.UpdateSeedOilAmount(-projectileSpeedUpgradeCost);
        _gunFiring.projectileSpeedOfWeapon += projectileSpeedUpgradeValue;
        
        if (_gunFiring.projectileSpeedOfWeapon > projectileSpeedMax) _gunFiring.projectileSpeedOfWeapon = projectileSpeedMax;
        if (projectileSpeedBar) projectileSpeedBar.fillAmount = _gunFiring.projectileSpeedOfWeapon / projectileSpeedMax;
    }
    
    public void UpgradePlayerHealth()
    {
        if (_playerHealthScript.currentHealth >= playerHealthMax || GameManager.Instance.seedOilAmount < playerHealthUpgradeCost) return;
        
        GameManager.Instance.UpdateSeedOilAmount(-playerHealthUpgradeCost);
        _playerHealthScript.Heal(playerHealthUpgradeValue);
        
        if (playerHealthBar) playerHealthBar.fillAmount = (float) _playerHealthScript.currentHealth / playerHealthMax;
    }

    public void BackToGuiMenu()
    {
        _manageSurvivalRounds.showGuiUpgrade = false;
        guiUpgrade.gameObject.SetActive(false);
        _manageSurvivalRounds.showGuiSurvivalRounds = true;
        survivalRoundManager.gameObject.SetActive(true);
    }
}