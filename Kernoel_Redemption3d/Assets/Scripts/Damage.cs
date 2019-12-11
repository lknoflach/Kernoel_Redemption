using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private CloneController clone;
    private PlayerScript player;
    private EnemyScript enemy;
    private ZombieScript zombie;

    public int damage = 50;
    // Start is called before the first frame update

    private void OnCollisionEnter(Collision col)
    {
        Debug.Log(gameObject.name + " collides with " + col.gameObject.name);
        if (col.gameObject.CompareTag("Clone"))
        {
            clone = col.gameObject.GetComponent<CloneController>();
            clone.health -= damage;
        }
        else if (col.gameObject.CompareTag("Player"))
        {
            player = col.gameObject.GetComponent<PlayerScript>();
            player.health -= damage;
        }
        else if (col.gameObject.CompareTag("Enemy"))
        {
            enemy = col.gameObject.GetComponent<EnemyScript>();
            enemy.health -= damage;
        }
        else if (col.gameObject.CompareTag("Zombie"))
        {
            zombie = col.gameObject.GetComponent<ZombieScript>();
            zombie.health -= damage;
        }

    }

    // Update is called once per frame
    void Update()
    {
    }
}