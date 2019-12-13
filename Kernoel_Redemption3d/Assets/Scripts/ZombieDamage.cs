using UnityEngine;

public class ZombieDamage : MonoBehaviour
{
    public int damage = 50;

    // Start is called before the first frame update
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Clone":
            case "Enemy":
            case "Player":
                // Zombie hits everyone
                var healthScript = other.gameObject.GetComponent<HealthScript>();
                healthScript.Damage(damage);
                break;
        }
    }
}