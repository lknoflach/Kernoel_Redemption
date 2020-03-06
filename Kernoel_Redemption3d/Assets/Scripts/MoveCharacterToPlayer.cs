using UnityEditor;
using UnityEngine;

public class MoveCharacterToPlayer : MonoBehaviour
{
    // Player Stuff
    private GameObject _player;
    private PlayerScript _playerScript;
    private CharacterMovement _playerMovement;
 
    // Character Stuff
    private Rigidbody _characterBody;
    
    [Header("Movement")]
    public bool isArrivedAtPlayer;
    public float currentMovementSpeed;
    public float movementSpeed = 5.0f;
    
    //public float movementFarThreshold = 20.0f;
    //public float movementShootThreshold = 10.0f;
    //public float movementCloseThreshold = 5.0f;

    [Header("Asleep")]
    public bool isAsleep;
    // ... in Degree
    public float fieldOfVision = 90.0f;
    // ... in ?
    public float maxVisionDistance = 50.0f;

    private void Start()
    {
        _player = GameObject.Find("PlayerHans");
        if (_player) _playerScript = _player.GetComponent<PlayerScript>();
        if (_player) _playerMovement = _player.GetComponent<CharacterMovement>();
        
        _characterBody = GetComponent<Rigidbody>();
        currentMovementSpeed = isAsleep ? 0f : movementSpeed;
    }

    private void OnCollisionEnter(Collision other)
    {
        CheckIfArrivedAtPlayer(other);
    }

    private void OnCollisionStay(Collision other)
    {
        CheckIfArrivedAtPlayer(other);
    }
    
    private void Update ()
    {
        if (!_player || !_characterBody) return;
        
        // Check if the Character is sleeping and can't see the Player
        if (isAsleep && !CanSeePlayer()) return;
        
        // Wake up the Character
        isAsleep = false;
        
        //ask if player moves
        if (_playerMovement.playerIsMoving)
        {
            isArrivedAtPlayer = false;
            currentMovementSpeed = movementSpeed;
        }
        
        // Check if the Character has already reached the Player
        if (isArrivedAtPlayer) return;
        
        // Move the Character to the Player
        MoveToPlayer();
    }

    private bool CanSeePlayer()
    {
        if (!_player) return false;
        
        // Is the Player in range?
        var distanceToPlayer = Vector3.Distance(_player.transform.position, transform.position);
        if (distanceToPlayer > maxVisionDistance) return false;
        
        // Is the Player in sight?
        var rayDirection = _player.transform.position - transform.position;
        if (Vector3.Angle(rayDirection, transform.forward) > fieldOfVision * 0.5f) return false;

        return true;
    }
    
    private void CheckIfArrivedAtPlayer(Collision other)
    {
        // Collision Character
        var target = other.gameObject;
        
        if (!isArrivedAtPlayer)
        {
            // Moving Character
            switch (gameObject.tag)
            {
                case "Clone":
                    // Clones try to reach the Player or a Clone of the Player
                    if (target.CompareTag("Player")) isArrivedAtPlayer = true;
                    if (target.CompareTag("Clone"))
                    {
                        var cloneMovement = target.GetComponent<MoveCharacterToPlayer>();
                        if (cloneMovement && cloneMovement.isArrivedAtPlayer)
                        {
                            if (_playerScript) _playerScript.AddCloneToPlayer(gameObject);
                            isArrivedAtPlayer = true;
                        }
                    }
                    break;
                
                case "Zombie":
                    // Zombies try to reach the Player
                    if (target.CompareTag("Player")) isArrivedAtPlayer = true;
                    break;
            }
        }

        // TODO check if this makes a problem for Zombies
        // Set the currentMovementSpeed to zero
        if (isArrivedAtPlayer) currentMovementSpeed = gameObject.CompareTag("Zombie") ? movementSpeed : 0f;
        }
    
    private void MoveToPlayer()
    {
        // Calculate player's position
        var direction = _player.transform.position - transform.position;
        var magnitude = direction.magnitude;
        direction.Normalize();
 
        // Calculate movementSpeed
        var velocity = direction * currentMovementSpeed;
 
        //if(magnitude > movementShootThreshold && magnitude < movementFarThreshold)
        //{
        
        // Move the Character
        _characterBody.velocity = new Vector3 (velocity.x, _characterBody.velocity.y, velocity.z);
        
        //}
        //else if(magnitude < movementCloseThreshold)
        //{
        //    _characterBody.velocity = new Vector3 (-velocity.x, _characterBody.velocity.y, -velocity.z);
        //}
 
        //if(magnitude < movementShootThreshold)
        //{
 
        //}
 
        // Face the Character to the Player
        transform.LookAt(new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z));
    }

    public bool IsMoving()
    {
        return currentMovementSpeed > 0f;
    }
}