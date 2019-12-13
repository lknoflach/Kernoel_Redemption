using UnityEngine;

/// <summary>
/// Handles the projectile lifetime and movement.
/// </summary>
public class ProjectileScript : MonoBehaviour
{
    /** PROJECTILE STUFF **/
    // Self-Destruction after X seconds
    private const int DestructionTimerInSeconds = 5;
    // Projectile Speed of the bullet
    public float projectileSpeed = 30;

    // Start is called before the first frame update
    private void Start()
    {
        // Destroy after X seconds to avoid any leak
        Destroy(gameObject, DestructionTimerInSeconds);
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Time.deltaTime * projectileSpeed * Vector3.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Destroy Projectile if we hit a Wall or Ground
        switch (other.gameObject.tag)
        {
            case "Ground":
            case "Wall":
                // destroy the Projectile because we hit the wall
                Destroy(gameObject);
                break;
        }
        
        // NOTE: Damage will be handled by the DamageScript
    }
}