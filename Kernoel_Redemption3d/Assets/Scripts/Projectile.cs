using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 firingPoint;

    [SerializeField]
    private float projectileSpeed;

    void Start()
    {
        firingPoint = transform.position;   
    }

    // Update is called once per frame
    void Update()
    {
         transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
      MoveProjectile();
    }

    void MoveProjectile(){
         
    }
}
