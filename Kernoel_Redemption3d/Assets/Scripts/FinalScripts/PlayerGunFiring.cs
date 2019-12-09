using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunFiring : MonoBehaviour
{
    [SerializeField]
    Transform firingPoint;

    
    [SerializeField]
    GameObject projectilePrefab;

    
    [SerializeField]
    float firingSpeed;

    public static PlayerGunFiring Instance;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = GetComponent<PlayerGunFiring>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot(){
        Instantiate(projectilePrefab, firingPoint.position, firingPoint.rotation);
    }
}
