using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the DamageTypes and determines if the damage should be applied to a GameObject.
/// </summary>
public class DamageScript : MonoBehaviour
{
    /** CHARACTER STUFF **/
    private enum CharacterTypes
    {
        Clone,
        Enemy,
        Player,
        Zombie,
        Undefined
    }

    // Defines the CharacterTypes for applying damage. 
    private readonly List<CharacterTypes> _validCharacterTypes;

    /** DAMAGE STUFF **/
    public int damage = 1;

    public bool isContinues;
    
    /// <summary>
    /// Cooldown in seconds between two damage (0 = no cooldown)
    /// </summary>
    public float damageApplyRate;

    /// <summary>
    /// Current cooldown in seconds until the next damage happens
    /// </summary>
    private float _damageCooldown;
    
    public enum DamageTypes
    {
        Enemy,
        Player,
        Trap,
        Zombie,
        Undefined
    }

    public DamageTypes damageType = DamageTypes.Undefined;

    public AudioSource audioData;
    
    public DamageScript()
    {
        _validCharacterTypes = new List<CharacterTypes>();
    }

    private void Start()
    {
        _damageCooldown = 0f;

        audioData = GetComponent<AudioSource>();
        
        // Determine which damageType will be applied to which characterTypes
        switch (damageType)
        {
            case DamageTypes.Enemy:
                // An Enemy can hit everyone EXCEPT other Enemies.
                _validCharacterTypes.Add(CharacterTypes.Clone);
                _validCharacterTypes.Add(CharacterTypes.Player);
                // validCharacterTypes.Add(CharacterTypes.Zombie);
                break;

            case DamageTypes.Player:
                // A Player can hit Enemies and Zombies.
                _validCharacterTypes.Add(CharacterTypes.Enemy);
                _validCharacterTypes.Add(CharacterTypes.Zombie);
                break;

            case DamageTypes.Trap:
                // A Trap can hit everyone.
                _validCharacterTypes.Add(CharacterTypes.Clone);
                _validCharacterTypes.Add(CharacterTypes.Enemy);
                _validCharacterTypes.Add(CharacterTypes.Player);
                _validCharacterTypes.Add(CharacterTypes.Zombie);
                break;

            case DamageTypes.Zombie:
                // A Zombie can hit everyone EXCEPT other Zombies.
                _validCharacterTypes.Add(CharacterTypes.Clone);
                // validCharacterTypes.Add(CharacterTypes.Enemy);
                _validCharacterTypes.Add(CharacterTypes.Player);
                break;
        }
    }

    private void Update()
    {
        if (_damageCooldown > 0f) _damageCooldown -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        ApplyDamage(other.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (isContinues) ApplyDamage(other.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        ApplyDamage(other.gameObject);
    }

    private void OnCollisionStay(Collision other)
    {
        if (isContinues) ApplyDamage(other.gameObject);
    }

    /// <summary>
    /// Apply the damage to the target's HealthScript if the targetCharacter is valid.
    /// </summary>
    /// <param name="target"></param>
    private void ApplyDamage(GameObject target)
    {
        if (!CanApplyDamage) return;

        var characterType = DetermineCharacterType(target);
        if (_validCharacterTypes.Contains(characterType))
        {
            // Check if there is a healthScript and apply Damage. 
            var healthScript = target.GetComponent<HealthScript>();
            if (healthScript)
            {
                // play damageSound if set
                if (audioData) audioData.Play(0);
                
                // Debug.Log(gameObject.name + ": inflicts damage<" + damage + "> on: " + target.name);
                healthScript.Damage(damage);
                // reset the damage timer
                _damageCooldown = damageApplyRate;
            }
        }
    }

    /// <summary>
    /// Returns the CharacterType from a GameObject.
    /// </summary>
    /// <param name="target"></param>
    private static CharacterTypes DetermineCharacterType(GameObject target)
    {
        var targetCharacter = CharacterTypes.Undefined;
        if (target.GetComponent<CloneScript>()) targetCharacter = CharacterTypes.Clone;
        if (target.GetComponent<EnemyScript>()) targetCharacter = CharacterTypes.Enemy;
        if (target.GetComponent<PlayerScript>()) targetCharacter = CharacterTypes.Player;
        if (target.GetComponent<ZombieScript>()) targetCharacter = CharacterTypes.Zombie;
        return targetCharacter;
    }

    /// <summary>
    /// Is the damage ready to apply?
    /// </summary>
    private bool CanApplyDamage => (_damageCooldown <= 0f);
}