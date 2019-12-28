using UnityEngine;

public class SpawnExplodedBarrel : MonoBehaviour
{
    public GameObject explodedBarrel;

    // Spawns an exploded barrel and a explosion when hit by bullet
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            SpawnBarrel();
        }
        else if(other.gameObject.CompareTag("ExplodingBarrel")){
            SpawnBarrel();
        }
    }
    
    
    private void SpawnBarrel(){
        Instantiate(explodedBarrel, transform.position, transform.rotation);
        Destroy(gameObject);
    }
   
}
