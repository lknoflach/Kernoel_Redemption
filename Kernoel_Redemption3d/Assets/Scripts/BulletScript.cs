using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed;
    public float maxDistance;
    private GameObject triggeringEnemy;
    private GameObject player;

    public float damage;
    // Start is called before the first frame update


    // Update is called once per frame
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.forward);
        maxDistance += 1 * Time.deltaTime;

        if (maxDistance >= 5)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            triggeringEnemy = other.gameObject;
            triggeringEnemy.GetComponent<EnemyScript>().health -= damage;
            Destroy(this.gameObject);
        }

        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            player.GetComponent<PlayerScript>().health -= damage;
        }
    }
}