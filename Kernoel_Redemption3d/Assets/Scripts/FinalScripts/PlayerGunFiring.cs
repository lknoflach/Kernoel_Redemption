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
