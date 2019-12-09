using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerScript : MonoBehaviour
{
    /** CHARACTER STUFF **/
    [FormerlySerializedAs("live")] public int health = 100;
    
    /** GUN STUFF **/
    public GameObject playerGun;
    private PlayerGunFiring playerGunFiringScript;
    
    /** MOVEMENT STUFF **/
    private Camera mainCamera;
    public Vector3 moveInput;
    public float moveSpeed;
    private Vector3 moveVelocity;
    private Rigidbody myRigidbody;

    /** CLONING STUFF **/
    // enables/disables cloning
    private bool isCloneable;
    // the prototype for new clone objects
    [FormerlySerializedAs("klones")] public GameObject clonePrototype;
    // the object which enables the cloning
    [FormerlySerializedAs("Station")] public GameObject cloningCapsule;
    // the array with all the following clones
    public GameObject[] clones;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();
        playerGunFiringScript = playerGun.GetComponent<PlayerGunFiring>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CloningCapsule"))
        {
            // enable cloning
            isCloneable = true;
        }

        if (other.gameObject.CompareTag("Clone"))
        {
            // add the clone to the list of clones
            clones.Append(other.gameObject);
        }
    }

    public void Update()
    {
        //cloning Button
        if (Input.GetKeyDown(KeyCode.E) && isCloneable == true)
        {
            Instantiate(clonePrototype, cloningCapsule.transform.position, Quaternion.identity);
            // disable cloning
            isCloneable = false;
        }

        if (health <= 0)
        {
            // we are still alive
        }

        // calculate movement
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * moveSpeed;

        var cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        var groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(cameraRay, out var rayLength)) ;
        {
            var pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
            // look to the cursor
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }

        if (Input.GetButtonDown("Fire1"))
        {
            playerGunFiringScript.Shoot();
        }
    }


    private void FixedUpdate()
    {
        // move
        myRigidbody.velocity = moveVelocity;
    }
}