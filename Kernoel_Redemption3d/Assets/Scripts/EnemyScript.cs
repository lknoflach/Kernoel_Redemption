﻿using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    /** CHARACTER STUFF **/
    public GameObject player;

    /** GUN STUFF **/
    public GameObject enemyGun;
    public bool isShooting;
    private GunFiring gunFiringScript;

    // The current Cooldown for the next shoot
    private float shootCooldown;

    // Cooldown in seconds between two shots
    public float shootingRate = 1f;

    public void Start()
    {
        player = GameObject.Find("PlayerHans");
        gunFiringScript = enemyGun.GetComponent<GunFiring>();
    }

    public void Update()
    {
        if (player)
        {
            // Focus on the Player
            // Debug.DrawLine(transform.position, player.transform.position, Color.red);

            transform.LookAt(player.transform);

            if (shootCooldown > 0f)
            {
                // Wait the shootCooldown timer
                shootCooldown -= Time.deltaTime;
                isShooting = false;
            }
            else
            {
                // Fire the gun
                gunFiringScript.Shoot();
                shootCooldown = shootingRate;
                isShooting = true;
            }
        }
    }
}