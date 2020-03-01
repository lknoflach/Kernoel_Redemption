using UnityEngine;

/// <summary>
/// Launch projectile
/// </summary>
public class GunFiring : MonoBehaviour
{
    /** CHARACTER STUFF **/
    private GameObject _player;
    AudioSource audioData;
    /** WEAPON STUFF **/
    public int damageOfWeapon = 1;
    public float projectileSpeedOfWeapon = 30; 
    // Starting Coordinates of the Projectile
    public Transform firingPoint;
    // The Projectile itself
    public GameObject projectilePrefab;

    public void Start()
    {
        _player = GameObject.Find("PlayerHans");
        audioData = GetComponent<AudioSource>();
    }

    public void Shoot()
    {
        // Create a Projectile
        var projectile = Instantiate(projectilePrefab, firingPoint.position, firingPoint.rotation);
        audioData.Play(0);

        //needed for upgrading the damage
        DamageScript damageScript = projectilePrefab.GetComponent<DamageScript>();
        damageScript.damage = damageOfWeapon;

        ProjectileScript projectileScript = projectilePrefab.GetComponent<ProjectileScript>();
        projectileScript.projectileSpeed = projectileSpeedOfWeapon;
        

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
    }
}