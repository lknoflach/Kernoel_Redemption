using UnityEngine;

/// <summary>
/// Launch projectile
/// </summary>
public class GunFiring : MonoBehaviour
{
    // Starting Coordinates of the Projectile
    [SerializeField]
    public Transform firingPoint;

    // The Projectile itself
    [SerializeField]
    public GameObject projectilePrefab;

    public void Shoot(){
        // Create a Projectile
        Instantiate(projectilePrefab, firingPoint.position, firingPoint.rotation);
    }
}
