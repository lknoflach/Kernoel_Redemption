using UnityEngine;

public class Explosion : MonoBehaviour
{
    //Destroys Traps and i we like something else in its area Damage Radius is 
    // by the sphere collider of the prefab the particle system is just eye-candy 
    // Grenade explodes after a time delay.
    public float fuseTime;

    void Start()
    {
        Debug.Log("Explode");

        var exp = GetComponent<ParticleSystem>();
        exp.Play();

        Destroy(gameObject, 2f);
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

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Explosion->OnCollisionEnter: other = " + other.gameObject.name);
        if (other.gameObject.CompareTag("Trap"))
        {
            Destroy(other.gameObject, 1f);
        }
    }
}