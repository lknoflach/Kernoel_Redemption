using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnExplosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    private void OnCollisionEnter(Collision other)
    {
    
        if(other.gameObject.CompareTag("Trap")){
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
