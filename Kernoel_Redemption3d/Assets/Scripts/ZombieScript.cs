using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    /** MOVEMENT STUFF **/
    public bool isArrivedAtPlayer;
    public bool isMoving;
    
    public float currentMovementSpeed = 10f;
    public float movementSpeed = 10;

    public float fieldOfViewDegrees = 90.0f;
    public float maxMovementRangeDistance = 50.0f;

    /** PLAYER STUFF **/
    private GameObject _player;
    private CharacterMovement _playerMovement;

    private void Start()
    {
        currentMovementSpeed = movementSpeed;
        
        _player = GameObject.Find("PlayerHans");
        if (_player) _playerMovement = _player.GetComponent<CharacterMovement>();
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("OnCollisionEnter: " + gameObject.name + ": Zombie searches on: " + other.gameObject.name);
        CheckIfArrivedAtPlayer(other);
    }

    private void CheckIfArrivedAtPlayer(Collision other)
    {
        if (isArrivedAtPlayer) return;
        
        isArrivedAtPlayer = other.gameObject.CompareTag("Player");

        if (isArrivedAtPlayer) currentMovementSpeed = 0f;
    }

    private void Update()
    { 
        if (CanSeePlayer()) MoveToPlayer();
    }

    private bool CanSeePlayer()
    {
        if (!_player) return false;
        
        // Is the Player in range?
        var distanceToPlayer = Vector3.Distance(_player.transform.position, transform.position);
        if (distanceToPlayer > maxMovementRangeDistance) return false;
        
        // Is the Player in sight?
        var rayDirection = _player.transform.position - transform.position;
        if (Vector3.Angle(rayDirection, transform.forward) > fieldOfViewDegrees * 0.5f) return false;

        return true;
    }

    private void MoveToPlayer()
    {
        if (!_player || !_playerMovement) return;
        
        //ask if player moves
        if (_playerMovement.playerIsMoving)
        {
            isArrivedAtPlayer = false;
            currentMovementSpeed = movementSpeed;
        }
        
        if (isArrivedAtPlayer) return;
        
        //var playerPosition = new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z);
        //transform.LookAt(playerPosition);
        transform.LookAt(_player.transform);

        transform.position += Time.deltaTime * movementSpeed * transform.forward;
        isMoving = true;
    }
}