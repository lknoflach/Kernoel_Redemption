﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDamage : MonoBehaviour
{
    private CloneController clone;
    private PlayerScript player;
    private EnemyScript enemy;

    public int damage = 50;
    // Start is called before the first frame update

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Clone"))
        {
            clone = col.gameObject.GetComponent<CloneController>();
            clone.health -= damage;
        }
        else if (col.gameObject.tag == "Player")
        {
            player = col.gameObject.GetComponent<PlayerScript>();
            player.health -= damage;
        }
        else if (col.gameObject.tag == "Enemy")
        {
            enemy = col.gameObject.GetComponent<EnemyScript>();
            enemy.health -= damage;
        }

    }

    // Update is called once per frame
    void Update()
    {
    }
}