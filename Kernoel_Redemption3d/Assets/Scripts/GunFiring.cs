using UnityEngine;

/// <summary>
/// Launch projectile
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class GunFiring : MonoBehaviour
{
    /** CHARACTER STUFF **/
    private GameObject _player;

    /** WEAPON STUFF **/
    private AudioSource _audioData;
    private DamageScript _damageScript;
    private ProjectileScript _projectileScript;
    
    public int damageOfWeapon = 1;

    public float projectileSpeedOfWeapon = 30;

    // Starting Coordinates of the Projectile
    public Transform firingPoint;

    // The Projectile itself
    public GameObject projectilePrefab;

    /// <summary>
    /// Cooldown between two attacks (0 = no cooldown)
    /// </summary>
    public float attackRate;

    /// <summary>
    /// Current cooldown in seconds until the next attack happens
    /// </summary>
    private float _attackCooldown;

    private void Start()
    {
        _attackCooldown = 0f;
        // init needed scripts
        _player = GameObject.Find("PlayerHans");
        _projectileScript = projectilePrefab.GetComponent<ProjectileScript>();
        _damageScript = projectilePrefab.GetComponent<DamageScript>();
        _audioData = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_attackCooldown > 0f) _attackCooldown -= Time.deltaTime;
    }

    public void Shoot()
    {
        if (!CanAttack) return;

        // Create a Projectile
        var projectile = Instantiate(projectilePrefab, firingPoint.position, firingPoint.rotation);
        // Play the sound of the gun
        if (_audioData && _audioData.isActiveAndEnabled && !_audioData.isPlaying) _audioData.Play(0);

        // do upgrades
        _damageScript.damage = damageOfWeapon;
        _projectileScript.projectileSpeed = projectileSpeedOfWeapon;

        var source = transform.parent.gameObject;
        switch (source.tag)
        {
            case "Enemy":
                // point enemy projectiles to the player position
                if (_player) projectile.transform.LookAt(_player.transform);
                break;

            case "Player":
                // point player projectiles to the cursor position
                var cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                var groundPlane = new Plane(Vector3.up, Vector3.zero);

                if (!groundPlane.Raycast(cameraRay, out var rayLength)) return;

                var pointToLook = cameraRay.GetPoint(rayLength);
                // Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
                projectile.transform.LookAt(new Vector3(pointToLook.x, projectile.transform.position.y, pointToLook.z));
                break;
        }

        // reset the attack timer
        _attackCooldown = attackRate;
    }

    /// <summary>
    /// Is the gun ready to shoot?
    /// </summary>
    private bool CanAttack => (_attackCooldown <= 0f);
}