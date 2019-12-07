using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float health;
    public GameObject player;
    public float waittime;
    private float currentwait;
    private bool shot;
    public GameObject bullet;
    public GameObject bulletSpwanPoint;
    private Transform bulletSpawn;
    public void Start()
    {
        player = GameObject.FindWithTag("Player");

        bulletSpwanPoint = GameObject.Find("GundHolder/bullte spwan point");
      
    }

    public void Update()
    {
        if(health <= 0)
        {
            Die();
        }
        this.transform.LookAt(player.transform);

        if(currentwait == 0)
        {
            shoot();
        }
        if(shot && currentwait < waittime)
        {
            currentwait += 1 * Time.deltaTime;
        }
        if(currentwait >= waittime)
        {
            currentwait = 0;
        }
    }

    public void Die()
    {
      
        Destroy(this.gameObject);

    }

    public void shoot()
    {
     shot = true;
  
        bulletSpawn =  Instantiate(bullet.transform, bulletSpwanPoint.transform.position,Quaternion.identity);
        bulletSpawn.rotation = this.transform.rotation;
    }
   
}
