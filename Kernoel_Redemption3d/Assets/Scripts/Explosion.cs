using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    
  //Destroys Traps and i we like something else in its area Damage Radius is 
  //by the spherecollider of the prefab the particle system is just eyecandy 
    // Grenade explodes after a time delay.
   public float fuseTime;

    void Start() {
        Debug.Log("Explode");
           
        var exp = GetComponent<ParticleSystem>();
        exp.Play();

        Destroy(gameObject, 2f);
    }
  
  //currently just destroys traps
    private void OnTriggerEnter(Collider other)
    {
            Debug.Log("lkjslkafdjlöadsfhjlkasdfhhydkjfhdklyjsfdhyfyf\nalsödjföladfhkhasdklfhasdkfhölkasf\nasldfhlksadfjh");
        if(other.gameObject.CompareTag("Trap")){
            Destroy(other.gameObject, 1f);
        }
    }
    private void OnCollisionEnter(Collider other)
    {
            Debug.Log("lkjslkafdjlöadsfhjlkasdfhhydkjfhdklyjsfdhyfyf\nalsödjföladfhkhasdklfhasdkfhölkasf\nasldfhlksadfjh");
        if(other.gameObject.CompareTag("Trap")){
            Destroy(other.gameObject, 1f);
        }
    }


    
}
