using System.Collections;
using System.Collections.Generic;
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

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        maxDistance += 1 * Time.deltaTime;

        if(maxDistance >= 5)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            triggeringEnemy = other.gameObject;
            triggeringEnemy.GetComponent<EnemyScript>().health -= damage;
            Destroy(this.gameObject);
        }

        if(other.tag == "Player")
        {
            player = other.gameObject;
            player.GetComponent<PlayerScript>().health -= damage;
        }
    }
}
