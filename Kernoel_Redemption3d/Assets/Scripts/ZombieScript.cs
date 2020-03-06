using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    private GameObject _player;
    private DamageScript _damageScript;
    private MoveCharacterToPlayer _moveZombieToPlayer;

    private void Start()
    {
        _player = GameObject.Find("PlayerHans");
        _damageScript = GetComponent<DamageScript>();
        _moveZombieToPlayer = GetComponent<MoveCharacterToPlayer>();
    }

    private void Update()
    {
        if (!_player || !_moveZombieToPlayer || !_damageScript) return;
        
        // Apply Damage if the Zombie reached the Player
        if (_moveZombieToPlayer.isArrivedAtPlayer) _damageScript.ApplyDamage(_player);
    }
}