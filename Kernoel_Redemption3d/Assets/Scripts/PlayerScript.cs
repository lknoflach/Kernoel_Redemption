using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    /** GUN STUFF **/
    public GameObject playerGun;
    private GunFiring gunFiringScript;
    
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
    public GameObject clonePrototype;
    // the object which enables the cloning
    public GameObject cloningCapsule;
    // the array with all the following clones
    public List<GameObject> clones = new List<GameObject>();

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();
        cloningCapsule.GetComponent<Transform>();
        gunFiringScript = playerGun.GetComponent<GunFiring>();
    }

    private void OnCollisionEnter(Collision other)
    {
        var target = other.gameObject;
        switch (target.tag)
        {
            case "Clone":
                // add the clone to the list of clones
                if (!clones.Contains(target)) clones.Add(target);
                break;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        var target = other.gameObject;
        switch (target.tag)
        {
            case "CloningCapsule":
                // enable cloning
                isCloneable = true;
                break;
        }
    }

    public void Update()
    {
        //cloning Button
        if (Input.GetKeyDown(KeyCode.E) && isCloneable)
        {
            Instantiate(clonePrototype, transform.position ,  transform.rotation);
            // disable cloning
            isCloneable = false;
        }

        // calculate movement
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * moveSpeed;

        var cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        var groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(cameraRay, out var rayLength))
        {
            var pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
            // look to the cursor
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }

        if (Input.GetButtonDown("Fire1"))
        {
            gunFiringScript.Shoot();
        }
    }

    private void FixedUpdate()
    {
        // move
        myRigidbody.velocity = moveVelocity;
    }
}