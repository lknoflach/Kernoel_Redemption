using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles health and apply damage.
/// </summary>
public class HealthScript : MonoBehaviour
{
    /** HEALTH STUFF **/
    public int currentHealth = 100;
    public int totalHealth = 100;
    
    [Header("Unity Stuff")] public Image healthBar;

    private void Start()
    {
        // Set currentHealth to totalHealth
        currentHealth = totalHealth;

        // Update the health bar
        if (healthBar) healthBar.fillAmount = ((float) currentHealth) / ((float) totalHealth);
    }

    /// <summary>
    /// Inflicts damage, updates the healthSlider and checks if the object should be destroyed
    /// </summary>
    /// <param name="damageCount"></param>
    public void Damage(int damageCount)
    {
        currentHealth -= damageCount;
        Debug.Log(gameObject.name + ": currentHealth<" + currentHealth.ToString() + ">");
        
        // Update the health bar
        if (healthBar) healthBar.fillAmount = ((float) currentHealth) / ((float) totalHealth);
        
        if (currentHealth <= 0)
        {
            // Dead!
            Destroy(gameObject);
        }
    }
}