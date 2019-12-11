using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    /** CHARACTER STUFF **/
    public int health = 100;
    public GameObject player;
    
    /** GUN STUFF **/
    public GameObject enemyGun;
    private GunFiring gunFiringScript;
    // The current Cooldown for the next shoot
    private float shootCooldown;
    // Cooldown in seconds between two shots
    public float shootingRate = 1f;

    public void Start()
    {
        player = GameObject.FindWithTag("Player");
        gunFiringScript = enemyGun.GetComponent<GunFiring>();
    }

    public void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        
        // Focus on the Player
        transform.LookAt(player.transform);
        
        if (shootCooldown > 0f)
        {
            // Wait the shootCooldown timer
            shootCooldown -= Time.deltaTime;
        }
        else
        {
            // Fire the gun
            gunFiringScript.Shoot();
            shootCooldown = shootingRate;
        }
    }
}