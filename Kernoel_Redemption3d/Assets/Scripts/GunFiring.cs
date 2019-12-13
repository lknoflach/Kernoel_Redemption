using UnityEngine;

/// <summary>
/// Launch projectile
/// </summary>
public class GunFiring : MonoBehaviour
{
    // Starting Coordinates of the Projectile
    public Transform firingPoint;
    // The Projectile itself
    public GameObject projectilePrefab;

    public void Shoot()
    {
        // var source = transform.parent.gameObject;
        // var playerScript = FindObjectOfType<PlayerScript>();
        // var playerPosition = playerScript.gameObject.transform.position;
        // Debug.DrawLine(firingPoint.position, playerPosition, Color.green);

        // Create a Projectile
        Instantiate(projectilePrefab, firingPoint.position, firingPoint.rotation);
    }
}
