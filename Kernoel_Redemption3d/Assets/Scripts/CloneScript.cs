using UnityEngine;

public class CloneScript : MonoBehaviour
{
    /** MOVEMENT STUFF **/
    public bool isArrivedAtPlayer;

    public float currentMovementSpeed = 10f;
    public float movementSpeed = 10f;

    /** PLAYER STUFF **/
    private GameObject _player;

    private CharacterMovement _characterMovement;

    private void Start()
    {
        //get Player to get the CharacterMovementScript
        _player = GameObject.Find("PlayerHans");
        //get CharacterMovementScript to check if Player is moving
        if (_player) _characterMovement = _player.GetComponent<CharacterMovement>();
        currentMovementSpeed = movementSpeed;
    }

    private void CheckIfArivedAtPlayer(Collision other)
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

    private void OnCollisionEnter(Collision other)
    {
        CheckIfArivedAtPlayer(other);
    }


    private void OnCollisionStay(Collision other)
    {
        CheckIfArivedAtPlayer(other);
    }

    private void Update()
    {
        if (!_player || !_characterMovement) return;
        // Debug.Log(playerScript.moveInput);

        //ask if player moves
        if (_characterMovement.playerIsMoving)
        {
            isArrivedAtPlayer = false;
            currentMovementSpeed = movementSpeed;
        }

        if (isArrivedAtPlayer) return;

        transform.LookAt(_player.transform.position);
        // transform.position += Time.deltaTime * currentMovementSpeed * transform.forward;
        transform.position += 0.02f * currentMovementSpeed * transform.forward;
    }
}