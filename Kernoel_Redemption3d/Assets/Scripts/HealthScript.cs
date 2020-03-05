using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
/// Handles health and apply damage.
/// </summary>
public class HealthScript : MonoBehaviour
{
    /** HEALTH STUFF **/
    [Header("Life")]
    public int currentHealth = 100;
    public int totalHealth = 100;

    [Header("Unity Stuff")]
    public Image healthBar;
    
    [Header("Death")]
    [FormerlySerializedAs("DeathEffect")]public GameObject deathEffect;
    [FormerlySerializedAs("timeOut")] public float deathEffectTimer = 3.0f;
    [FormerlySerializedAs("Kernoil")] public GameObject seedOil;
    [FormerlySerializedAs("probabilityOfMutationGoodSeedOil")] public float seedOilDropChance;
    
    private void Start()
    {
        // Set currentHealth to totalHealth
        currentHealth = totalHealth;

        // Update the health bar
        if (healthBar) healthBar.fillAmount = (float) currentHealth / totalHealth;
    }

    private static bool RandomBoolean(float likelinessInPercent)
    {
        // return true if the random number between 0 and 100 is smaller then your likeliness for a zombie
        return (Random.Range(0f, 100f) > likelinessInPercent);
    }

    /// <summary>
    /// Inflicts damage, updates the healthSlider and checks if the object should be destroyed
    /// </summary>
    /// <param name="damageCount"></param>
    /// 
    public void Damage(int damageCount)
    {
        currentHealth -= damageCount;
        //Debug.Log(gameObject.name + ": currentHealth<" + currentHealth.ToString() + ">");

        // Update the health bar
        if (healthBar) healthBar.fillAmount = (float) currentHealth / totalHealth;

        if (currentHealth > 0) return;
        
        // Remove Clone from Player
        if(gameObject.CompareTag("Clone"))
        {
            var player = GameObject.Find("PlayerHans");
            if (player) player.GetComponent<PlayerScript>().RemoveCloneFromPlayer(gameObject);
        }
        
        // Dead!
       var isGameOver = gameObject.name == "PlayerHans";


       var spawnPosition = transform.position;
       spawnPosition.z -= 3;

        // Show Death Effect if present
        if (deathEffect)
        {
            var instance = Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(instance.gameObject, deathEffectTimer);
        }

        Destroy(gameObject);

        // Spawn Seed Oil if present and there is a lucky fairy which says YES!
        if (seedOil && RandomBoolean(seedOilDropChance))
        {
            Instantiate(seedOil, spawnPosition, transform.rotation);
        }

        if (!isGameOver) return;
        
        // Set activeScene as currentScene before loading GameOverMenu
        var activeScene = SceneManager.GetActiveScene();
        GameManager.Instance.currentSceneName = activeScene.name;
        GameManager.Instance.LoadGameOverMenu();
    }

    public void Heal(int healCount)
    {
        if (currentHealth == totalHealth) return;
        
        currentHealth += healCount;
        
        if (currentHealth > totalHealth) currentHealth = totalHealth;
        
        // Update the health bar
        if (healthBar) healthBar.fillAmount = (float) currentHealth / totalHealth;
    }

    /*private void OnGUI()
    {
        // [CK] Another way to display a health bar would be this
        var targetPos = Camera.main.WorldToScreenPoint(transform.position);
        GUI.Box(new Rect(targetPos.x, Screen.height - targetPos.y, 60, 20), currentHealth + "/" + totalHealth);
    }*/
}