using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    /** GUN STUFF **/
    public GameObject playerGun;

    private GunFiring _gunFiringScript;

    /** MOVEMENT STUFF **/
    private Camera _mainCamera;
    private float _pushPower = 2f;

    // the array with all the following clones
    public List<GameObject> clones = new List<GameObject>();

    public void Start()
    {
        _mainCamera = FindObjectOfType<Camera>();
        _gunFiringScript = playerGun.GetComponent<GunFiring>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var body = hit.collider.attachedRigidbody;

        // No Rigidbody
        if (body == null || body.isKinematic) return;
        
        // We dont want to push objects below us
        if (hit.moveDirection.y <= -0.3f) return;
        
        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        var pushDirection = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        
        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.
        // Apply the push
        body.velocity = pushDirection * _pushPower;
    }

    private void OnCollisionEnter(Collision other)
    {
        var target = other.gameObject;
        switch (target.tag)
        {
            case "Clone":
                // add the clone to the list of clones
                AddCloneToPlayer(target);
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

        // Use this to enable auto-fire (not recommended): Input.GetButton("Fire1")
        if (Input.GetButtonDown("Fire1"))
        {
            _gunFiringScript.Shoot();
        }
    }

    public void AddCloneToPlayer(GameObject clone)
    {
        if (clones.Contains(clone)) return;
        
        clones.Add(clone);
        GameManager.Instance.UpdateCloneAmount(1);
    }

    public void RemoveCloneFromPlayer(GameObject clone)
    {
        if (!clones.Contains(clone)) return;

        clones.Remove(clone);
        GameManager.Instance.UpdateCloneAmount(-1);
    }
}