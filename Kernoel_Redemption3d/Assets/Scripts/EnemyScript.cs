using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    /** CHARACTER STUFF **/
    public GameObject player;

    /** GUN STUFF **/
    public GameObject enemyGun;

    public bool isShooting;
    private GunFiring _gunFiringScript;

    // The current Cooldown for the next shoot
    private float _shootCooldown;

    // Cooldown in seconds between two shots
    public float shootingRate = 1f;

    public void Start()
    {
        player = GameObject.Find("PlayerHans");
        _gunFiringScript = enemyGun.GetComponent<GunFiring>();
    }

    public void Update()
    {
        if (!player) return;
        // Focus on the Player
        // Debug.DrawLine(transform.position, player.transform.position, Color.red);

        transform.LookAt(player.transform);

        if (_shootCooldown > 0f)
        {
            // Wait the shootCooldown timer
            _shootCooldown -= Time.deltaTime;
            isShooting = false;
        }
        else
        {
            // Fire the gun
            _gunFiringScript.Shoot();
            _shootCooldown = shootingRate;
            isShooting = true;
        }
    }
}