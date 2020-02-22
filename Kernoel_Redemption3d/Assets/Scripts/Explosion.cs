using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    //Destroys Traps and i we like something else in its area Damage Radius is 
    // by the sphere collider of the prefab the particle system is just eye-candy 
    // Grenade explodes after a time delay.
    public float fuseTime;
    public float growspeed = 2f;
    private SphereCollider areaOfDetection;
    public ParticleSystem smoke;
    
    void Start()
    {
        Debug.Log("Explode");
        areaOfDetection = gameObject.GetComponent<SphereCollider>();
        
        var exp = GetComponent<ParticleSystem>();
     //   var smoke = GetComponent<Smoke_Chimny>();

        exp.Play();
        smoke.Play();
        Destroy(gameObject, 3f);
    }

    //currently just destroys traps
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Explosion->OnTriggerEnter: other = " + other.gameObject.name);
        if (other.gameObject.CompareTag("Trap"))
        {
            Destroy(other.gameObject, 1f);
        }
    }
    
    void Update() {
        // expand explosion collider radius
        areaOfDetection.radius = areaOfDetection.radius + growspeed* Time.deltaTime;
    }
  
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Explosion->OnCollisionEnter: other = " + other.gameObject.name);
        if (other.gameObject.CompareTag("Trap"))
        {
            Destroy(other.gameObject, 1f);
        }
    }
}