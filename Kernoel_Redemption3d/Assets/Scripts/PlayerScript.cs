using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //Variables 

    public float movementSpeed;
    public new GameObject camera;
    public GameObject playerObj;
    public GameObject bulletSpawnPoint;
    public float waitTime;
    public GameObject bullet;
    private Transform bulletSpawn;
    public float maxHealth;

    public float health = 0;
    private Camera mainCamera;

    // Update is called once per frame
    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        //Mouse Focus
        var playerPlane = new Plane(Vector3.up, transform.position);
        if (mainCamera)
        {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (playerPlane.Raycast(ray, out var hitDist))
            {
                var targetPoint = ray.GetPoint(hitDist);
                var targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
                targetRotation.x = 0;
                targetRotation.z = 0;
                playerObj.transform.rotation =
                    Quaternion.Slerp(playerObj.transform.rotation, targetRotation, 7f * Time.deltaTime);
            }
        }

        // Movement
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Time.deltaTime * movementSpeed * Vector3.forward);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Time.deltaTime * movementSpeed * Vector3.back);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Time.deltaTime * movementSpeed * Vector3.left);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Time.deltaTime * movementSpeed * Vector3.right);
        }

        //Shooting
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        var rotation = bulletSpawnPoint.transform.rotation;
        bulletSpawn = Instantiate(bullet.transform, bulletSpawnPoint.transform.position, rotation);
        bulletSpawn.rotation = rotation;
    }

    void Die()
    {
    }
}