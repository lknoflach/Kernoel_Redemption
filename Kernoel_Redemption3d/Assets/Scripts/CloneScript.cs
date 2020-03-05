using UnityEngine;

public class CloneScript : MonoBehaviour
{
    /** MOVEMENT STUFF **/
    public bool isArrivedAtPlayer;

    public float currentMovementSpeed = 10f;
    public float movementSpeed = 10f;

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
}