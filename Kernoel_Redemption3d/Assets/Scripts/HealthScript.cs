using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Handles health and apply damage.
/// </summary>
public class HealthScript : MonoBehaviour
{
    /** HEALTH STUFF **/
    public int currentHealth = 100;
    public int totalHealth = 100;
    public GameObject DeathEffect;
    public float timeOut = 3.0f;
    public CloningScript kernoil;
    public float probabilityOfMutationGoodSeedOil;
    [Header("Unity Stuff")] public Image healthBar;
    public GameObject Kernoil;
    public GameObject noKernoil;

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
    /// 
    private static bool RandomBoolean(float likelinessInPercent)
    {
        // return true if the random number between 0 and 100 is smaller then your likeliness for a zombie
        return (Random.Range(0f, 100f) > likelinessInPercent);
    }

    public void Damage(int damageCount)
    {
        currentHealth -= damageCount;
        //Debug.Log(gameObject.name + ": currentHealth<" + currentHealth.ToString() + ">");

        // Update the health bar
        if (healthBar) healthBar.fillAmount = ((float) currentHealth) / ((float) totalHealth);

        if (currentHealth > 0) return;

       // Dead!
         var isGameOver = gameObject.name == "PlayerHans";

        var spawnPosition = transform.position;
        spawnPosition.z -= 3;

        var instance = Instantiate(DeathEffect, transform.position, transform.rotation);
        Destroy(instance.gameObject, timeOut);
        Destroy(gameObject);

        Instantiate(RandomBoolean(probabilityOfMutationGoodSeedOil) ? Kernoil : noKernoil, spawnPosition, transform.rotation);



        if (isGameOver)
        {
            // Set activeScene as currentScene before loading GameOverMenu
            var activeScene = SceneManager.GetActiveScene();
            GameManager.Instance.currentSceneName = activeScene.name;
            GameManager.Instance.LoadGameOverMenu();
        }
    }

    private void OnGUI()
    {
        // [CK] Another way to display a health bar would be this
        // var targetPos = Camera.main.WorldToScreenPoint(transform.position);
        // GUI.Box(new Rect(targetPos.x, Screen.height - targetPos.y, 60, 20), currentHealth + "/" + totalHealth);
    }
}