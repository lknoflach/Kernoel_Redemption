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
    public Slider healthSlider;
    
    private void Start()
    {
        // Set currentHealth to totalHealth
        currentHealth = totalHealth;
        
        // Update the health slider
        if (healthSlider)
        {
            healthSlider.value = currentHealth;
        }
    }

    /// <summary>
    /// Inflicts damage, updates the healthSlider and checks if the object should be destroyed
    /// </summary>
    /// <param name="damageCount"></param>
    public void Damage(int damageCount)
    {
        currentHealth -= damageCount;
        Debug.Log(gameObject.name + ": currentHealth<" + currentHealth + ">");
        // Update the health slider
        if (healthSlider) healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            // Dead!
            Destroy(gameObject);
        }
    }
}