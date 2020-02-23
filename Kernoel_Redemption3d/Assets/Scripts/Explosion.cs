using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Destroys Traps and if we like something else in its area Damage Radius is 
    // by the sphere collider of the prefab the particle system is just eye-candy 
    // Grenade explodes after a time delay.
    public float fuseTime;
    public float growSpeed = 2f;
    private SphereCollider _areaOfDetection;
    public ParticleSystem smoke;

    private void Start()
    {
        Debug.Log("Explode");
        _areaOfDetection = gameObject.GetComponent<SphereCollider>();

        var exp = GetComponent<ParticleSystem>();
        // var smoke = GetComponent<SmokeChimney>();

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

    private void Update()
    {
        // expand explosion collider radius
        _areaOfDetection.radius = _areaOfDetection.radius + growSpeed * Time.deltaTime;
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