using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    /** CHARACTER STUFF **/
    private GameObject _player;

    /** GUN STUFF **/
    public GameObject enemyGun;

    public bool isShooting;
    private GunFiring _gunFiringScript;

    // The current Cooldown for the next shoot
    private float _shootCooldown;

    // Cooldown in seconds between two shots
    public float shootingRate = 1f;

    public float fieldOfViewDegrees = 180.0f;
    public float maxShootingDistance = 50.0f;
    
    public void Start()
    {
        _player = GameObject.Find("PlayerHans");
        _gunFiringScript = enemyGun.GetComponent<GunFiring>();
    }

    public void Update()
    {
        if (!_player) return;
        
        // Focus on the Player
        transform.LookAt(_player.transform);

        if (_shootCooldown > 0f)
        {
            // Wait the shootCooldown timer
            _shootCooldown -= Time.deltaTime;
            isShooting = false;
        }
        else
        {
            if (!CanSeePlayer()) return;
            
            // Fire the gun
            _gunFiringScript.Shoot();
            _shootCooldown = shootingRate;
            isShooting = true;
        }
    }

    private bool CanSeePlayer()
    {
        if (!_player) return false;
        
        // Is the Player in range?
        var distanceToPlayer = Vector3.Distance(_player.transform.position, transform.position);
        if (distanceToPlayer > maxShootingDistance) return false;

        // Is the Player in sight?
        var rayDirection = _player.transform.position - transform.position;
        if (Vector3.Angle(rayDirection, transform.forward) > fieldOfViewDegrees * 0.5f) return false;

        // Is something blocking the view?
        RaycastHit hit; 
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit);
        // If we see a Clone or the Player then we are ready to shoot!
        return hit.collider.CompareTag("Clone") || hit.collider.CompareTag("Player");
    }
}