using System.Collections.Generic;
using UnityEngine;


public class PlayerScript : MonoBehaviour
{
    /** GUN STUFF **/
    public GameObject playerGun;

    private GunFiring _gunFiringScript;

    /** MOVEMENT STUFF **/
    private Camera _mainCamera;

    public CharacterMovement characterMovement;

    // the array with all the following clones
    public List<GameObject> clones = new List<GameObject>();

    public void Start()
    {
        characterMovement = GetComponent<CharacterMovement>();
        _mainCamera = FindObjectOfType<Camera>();
        _gunFiringScript = playerGun.GetComponent<GunFiring>();
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

    public void Update()
    {
        // look to the cursor
        var cameraRay = _mainCamera.ScreenPointToRay(Input.mousePosition);
        var groundPlane = new Plane(Vector3.up, Vector3.zero);
        if (groundPlane.Raycast(cameraRay, out var rayLength))
        {
            var pointToLook = cameraRay.GetPoint(rayLength);
            // Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }

        if (Input.GetButtonDown("Fire1"))
        {
            _gunFiringScript.Shoot();
        }
    }
}