using UnityEngine;

public class PlayerGunFiring : MonoBehaviour
{
    [SerializeField]
    Transform firingPoint;

    
    [SerializeField]
    GameObject projectilePrefab;

    
    [SerializeField]
    float firingSpeed = 10;

    private static PlayerGunFiring instance;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = GetComponent<PlayerGunFiring>();   
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void Shoot(){
        Instantiate(projectilePrefab, firingPoint.position, firingPoint.rotation);
    }
}
