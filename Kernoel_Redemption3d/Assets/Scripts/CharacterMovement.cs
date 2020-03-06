using UnityEngine;

/// <summary>
/// This script moves the character controller forward and sideways based on the arrow keys. It also jumps when
/// pressing space. Make sure to attach a character controller to the same game object. It is recommended that you
/// make only one call to Move or SimpleMove per frame.
/// </summary>
public class CharacterMovement : MonoBehaviour
{
    private CharacterController _characterController;

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public bool playerIsMoving;
    public bool playerIsJumping;
    private Vector3 _moveDirection = Vector3.zero;
    public Vector3 move = Vector3.zero;
    AudioSource audioData;
    public GameObject flashlight;
    private bool flashlight_active = true;
    
    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        audioData = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_characterController.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes
            _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            _moveDirection *= speed;
            if (Input.GetButton("Jump"))
            {
                _moveDirection.y = jumpSpeed;
            }
          
        }

        if(Input.GetKeyDown(KeyCode.F)){
            if(flashlight_active){
                flashlight.gameObject.SetActive(false);
                flashlight_active = false;
            }else{
                flashlight.gameObject.SetActive(true);
                flashlight_active = true;
            }
        }


        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        _moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        move = _moveDirection * Time.deltaTime;

        //check if player is moving for clone movement, animation etc.
        if (!Mathf.Approximately(move.x, 0.0f) ||
            !Mathf.Approximately(move.z, 0.0f))
        {
            playerIsMoving = true;
           
        }
        else
        {
            playerIsMoving = false;
            if (audioData && audioData.isActiveAndEnabled) audioData.Play(0);
        }

        _characterController.Move(move);
    }
}