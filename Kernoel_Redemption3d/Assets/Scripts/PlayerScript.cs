using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //Variables 

    public float movementSpeed;
    public GameObject camera;
    public GameObject PlayerObj;
    public GameObject bulletSpwanPoint;
    public float waitTime;
    public GameObject bullet;
    private Transform bulletSpawn;
    public float maxHealth;
    public float health = 0;
    // Update is called once per frame
    void Update()
    {
        //Mouse Focus
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDist = 0.0f;

        if(playerPlane.Raycast(ray, out hitDist))
        {
            Vector3 targetPoint = ray.GetPoint(hitDist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            PlayerObj.transform.rotation = Quaternion.Slerp(PlayerObj.transform.rotation, targetRotation, 7f * Time.deltaTime);
        }
        // Movement
        if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
        }

        //Shooting
        if(Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        bulletSpawn = Instantiate(bullet.transform, bulletSpwanPoint.transform.position, bulletSpwanPoint.transform.rotation);
        bulletSpawn.rotation = bulletSpwanPoint.transform.rotation;  
    }

    void Die()
    {

    }
}
