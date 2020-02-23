using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    /** MOVEMENT STUFF **/
    public bool isArrivedAtPlayer = false;

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
        var target = other.gameObject;
        if (!isArrivedAtPlayer)
        {
            switch (target.tag)
            {
                case "Clone":
                    var cloneScript = target.GetComponent<CloneScript>();
                    if (cloneScript && cloneScript.isArrivedAtPlayer)
                    {
                        Debug.Log("Zombie is arrived at other Clone");
                        isArrivedAtPlayer = true;
                    }

                    break;

                case "Player":
                    Debug.Log("Zombie is arrived at Player");
                    isArrivedAtPlayer = true;
                    break;
            }
        }
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
        if (_player)
        {
            // RaycastHit hit;
            var rayDirection = _player.transform.position - transform.position;
            if ((Vector3.Angle(rayDirection, transform.forward)) <= fieldOfViewDegrees * 0.5f)
            {
                //Debug.Log("test");
                // Detect if player is within the field of view
                // if (Physics.Raycast(transform.position, rayDirection, out hit, visibilityDistance))
                // {
                return true;
                // return (hit.transform.CompareTag("Player"));
                // }
            }
        }

        return false;
    }

    private void MoveToPlayer()
    {
        if (_player)
        {
            transform.LookAt(_player.transform.position);
            transform.position += Time.deltaTime * movementSpeed * transform.forward;
            isMoving = true;
        }
    }
}