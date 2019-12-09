using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    private CloneController clone;
    private PlayerScript player;
    public int damage = 50;
    // Start is called before the first frame update
   
    void OnCollisionEnter(Collision col){
        if(col.gameObject.tag == "Clone"){
            clone = col.gameObject.GetComponent<CloneController>();
            clone.health = clone.health - damage;
        }else if(col.gameObject.name == "PlayerCube"){
            player = col.gameObject.GetComponent<PlayerScript>();
            player.health = player.health - damage;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
