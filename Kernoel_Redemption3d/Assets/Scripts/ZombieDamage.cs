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
                var clone = other.gameObject.GetComponent<CloneController>();
                clone.health -= damage;
                break;
                
            case "Player":
                var player = other.gameObject.GetComponent<PlayerScript>();
                player.health -= damage;
                break;
                
            case "Enemy":
                var enemy = other.gameObject.GetComponent<EnemyScript>();
                enemy.health -= damage;
                break;
        }
    }
}