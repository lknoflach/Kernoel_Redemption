using UnityEngine;

public class CloneScript : MonoBehaviour
{
    /** MOVEMENT STUFF **/
    public bool isArrivedAtPlayer;
    
    public bool isAsleep;
    public float fieldOfViewDegrees = 90.0f;
    public float maxMovementRangeDistance = 50.0f;
    
    public float currentMovementSpeed = 10f;
    public float movementSpeed = 10f;

    /** PLAYER STUFF **/
    private GameObject _player;
    private CharacterMovement _playerMovement;

    private void Start()
    {
        if (!isAsleep) currentMovementSpeed = movementSpeed;
        
        _player = GameObject.Find("PlayerHans");
        if (_player) _playerMovement = _player.GetComponent<CharacterMovement>();
    }

    private void OnCollisionEnter(Collision other)
    {
        CheckIfArrivedAtPlayer(other);
    }

    private void OnCollisionStay(Collision other)
    {
        CheckIfArrivedAtPlayer(other);
    }

    private void CheckIfArrivedAtPlayer(Collision other)
    {
        if (!isArrivedAtPlayer)
        {
            var target = other.gameObject;
            switch (target.tag)
            {
                case "Clone":
                    var cloneScript = target.GetComponent<CloneScript>();
                    if (cloneScript && cloneScript.isArrivedAtPlayer)
                    {
                        // Debug.Log("Clone is arrived at other Clone");
                        _player.GetComponent<PlayerScript>().AddCloneToPlayer(gameObject);
                        isArrivedAtPlayer = true;
                    }

                    break;

                case "Player":
                    // Debug.Log("Clone is arrived at Player");
                    isArrivedAtPlayer = true;
                    break;
            }
        }

        if (isArrivedAtPlayer) currentMovementSpeed = 0f;
    }

    private void Update()
    {
        if (!_player || !_playerMovement) return;
        // Debug.Log(playerScript.moveInput);

        if (isAsleep && !CanSeePlayer()) return;
        isAsleep = false;
        
        //ask if player moves
        if (_playerMovement.playerIsMoving)
        {
            isArrivedAtPlayer = false;
            currentMovementSpeed = movementSpeed;
        }

        if (isArrivedAtPlayer) return;

        var playerPosition = new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z);
        transform.LookAt(playerPosition);
        //transform.LookAt(_player.transform.position);
        
        transform.position += Time.deltaTime * currentMovementSpeed * transform.forward;
        // transform.position += 0.02f * currentMovementSpeed * transform.forward;
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
}