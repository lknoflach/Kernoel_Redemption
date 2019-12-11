using System;
using UnityEngine;

/// <summary>
/// Projectile behavior
/// </summary>
public class ShotScript : MonoBehaviour
{
    // Self-Destruction after X seconds
    private const int DestructionTimerInSeconds = 5;

    // Projectile Speed of the bullet
    [SerializeField] public float projectileSpeed = 30;

    // Start is called before the first frame update
    private void Start()
    {
        // Destroy after X seconds to avoid any leak
        Destroy(gameObject, DestructionTimerInSeconds);
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Time.deltaTime * projectileSpeed * Vector3.forward);
    }
}