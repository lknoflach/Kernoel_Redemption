﻿using System;
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

    public enum DamageTypes
    {
        Enemy,
        Player,
        Trap,
        Zombie,
        Undefined
    }

    public DamageTypes damageType = DamageTypes.Undefined;

    public DamageScript()
    {
        _validCharacterTypes = new List<CharacterTypes>();
    }

    private void Start()
    {
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

    /// <summary>
    /// Apply the damage to the target's HealthScript if the targetCharacter is valid.
    /// </summary>
    /// <param name="target"></param>
    private void ApplyDamage(GameObject target)
    {
        var characterType = DetermineCharacterType(target);
        if (_validCharacterTypes.Contains(characterType))
        {
            // Check if there is a healthScript and apply Damage. 
            var healthScript = target.GetComponent<HealthScript>();
            if (healthScript)
            {
                //Debug.Log(gameObject.name + ": inflicts damage<" + damage + "> on: " + target.name);
                healthScript.Damage(damage);
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
}