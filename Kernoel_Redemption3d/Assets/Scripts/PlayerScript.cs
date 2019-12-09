
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject playerGun;
    private PlayerGunFiring PlayerGunFiringScript;
    public float moveSpeed;
    private Rigidbody myRigidbody;
    private bool clones;
    private Vector3 moveInput;


    public Vector3 moveInput;
    private Vector3 moveVelocity;
    public int live = 100; 

    private Camera mainCamera;
    public GameObject klones;
    public GameObject Station;
    public GameObject Player;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();
        PlayerGunFiringScript = playerGun.GetComponent<PlayerGunFiring>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CloningCapsule")
        {
            clones = true;
        }   
    }

    public void Update()
    {

      
        //cloning Button
        if (Input.GetKeyDown(KeyCode.E) && clones == true)
        {
            GameObject.Instantiate(klones, Station.transform.position, Quaternion.identity);
            clones = false;

        if(live <= 0){

        }

        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * moveSpeed;

        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength));
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
            transform.LookAt(new Vector3(pointToLook.x, pointToLook.y, pointToLook.z));
        }
        if(Input.GetButtonDown("Fire1")){
            PlayerGunFiringScript.Shoot();
        }
        
    }
    
   

    private void FixedUpdate()
    {
        myRigidbody.velocity = moveVelocity;
    }


}
