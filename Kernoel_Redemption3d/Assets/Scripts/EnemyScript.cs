using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float health;
    public GameObject player;
    public float waitTime;
    private float currentWait;
    private bool shot;
    public GameObject bullet;
    public GameObject bulletSpawnPoint;
    private Transform bulletSpawn;

    public void Start()
    {
        player = GameObject.FindWithTag("Player");

        // bulletSpawnPoint = GameObject.Find("GunHolder");
    }

    public void Update()
    {
        if (health <= 0)
        {
            Die();
        }

        this.transform.LookAt(player.transform);

        if (currentWait == 0)
        {
            Shoot();
        }

        if (shot && currentWait < waitTime)
        {
            currentWait += 1 * Time.deltaTime;
        }

        if (currentWait >= waitTime)
        {
            currentWait = 0;
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }

    private void Shoot()
    {
        shot = true;

        bulletSpawn = Instantiate(bullet.transform, bulletSpawnPoint.transform.position, Quaternion.identity);
        bulletSpawn.rotation = this.transform.rotation;
    }
}