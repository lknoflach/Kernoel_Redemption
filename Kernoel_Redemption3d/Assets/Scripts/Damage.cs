using UnityEngine;

public class Damage : MonoBehaviour
{
    // Initial Damage
    public int damage = 50;

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Clone":
                Debug.Log(gameObject.name + " triggers with " + other.gameObject.name);
                if (gameObject.name.Contains("BulletEnemy"))
                {
                    var clone = other.gameObject.GetComponent<CloneController>();
                    clone.health -= damage;
                }
                break;

            case "Enemy":
                Debug.Log(gameObject.name + " triggers with " + other.gameObject.name);
                if (gameObject.name.Contains("BulletPlayer"))
                {
                    var enemy = other.gameObject.GetComponent<EnemyScript>();
                    enemy.health -= damage; 
                }
                break;
            
            case "Player":
                Debug.Log(gameObject.name + " triggers with " + other.gameObject.name);
                if (gameObject.name.Contains("BulletEnemy"))
                {
                    var player = other.gameObject.GetComponent<PlayerScript>();
                    player.health -= damage;
                }

                break;
            
            case "Zombie":
                Debug.Log(gameObject.name + " triggers with " + other.gameObject.name);
                if (gameObject.name.Contains("BulletPlayer"))
                {
                    var zombie = other.gameObject.GetComponent<ZombieScript>();
                    zombie.health -= damage;
                }

                break;
        }
    }
}