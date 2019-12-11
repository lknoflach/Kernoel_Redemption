using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 firingPoint;

    [SerializeField] float projectileSpeed = 30;

    private void Start()
    {
        firingPoint = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Time.deltaTime * projectileSpeed * Vector3.forward);
    }

    private void OnCollisionEnter(Collision col)
    {
        Destroy(gameObject);
    }
}