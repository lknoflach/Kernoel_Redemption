using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnExplodedBarrel : MonoBehaviour
{
    public GameObject BarrelExploding;

    // Spawns an exploded barrel and a explosion when hit by bullet
  

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            spwanExplodedBarrel();
        }
        else if(other.gameObject.CompareTag("ExplodingBarrel")){
            spwanExplodedBarrel();
        }
       //
    }
    
    
    private void spwanExplodedBarrel(){
        Instantiate(BarrelExploding, transform.position, transform.rotation);
        Destroy(gameObject);
    }
   
}
