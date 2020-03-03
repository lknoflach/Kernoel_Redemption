using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    /** MOVEMENT STUFF **/
    public bool isArrivedAtPlayer;
    public bool isMoving;
    public float movementSpeed = 10;
    public bool moveOnlyOnSight = true;
    public float fieldOfViewDegrees = 90.0f;
    public float visibilityDistance = 200000.0f;

    /** PLAYER STUFF **/
    private GameObject _player;
    private CharacterMovement _playerMovement;

    private void Start()
    {
        _player = GameObject.Find("PlayerHans");
        if (_player) _playerMovement = _player.GetComponent<CharacterMovement>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isArrivedAtPlayer) return;
        
        isArrivedAtPlayer = other.gameObject.CompareTag("Player");
    }

    private void Update()
    {
        if (!moveOnlyOnSight)
        {
            // Debug.Log(playerScript.moveInput);
            if (!Mathf.Approximately(_playerMovement.move.x, 0.0f) ||
                !Mathf.Approximately(_playerMovement.move.z, 0.0f))
            {
                isArrivedAtPlayer = false;
            }

            if (!isArrivedAtPlayer) MoveToPlayer();
        }
        else
        {
            // Debug.Log(CanSeePlayer());
            if (CanSeePlayer())
            {
                MoveToPlayer();
            }
        }
    }

    private bool CanSeePlayer()
    {
        if (!_player) return false;
        
        // RayCast hit
        var rayDirection = _player.transform.position - transform.position;
        return (Vector3.Angle(rayDirection, transform.forward)) <= fieldOfViewDegrees * 0.5f;
    }

    private void MoveToPlayer()
    {
        if (!_player) return;
        
        transform.LookAt(_player.transform.position);
        transform.position += Time.deltaTime * movementSpeed * transform.forward;
        isMoving = true;
    }
}