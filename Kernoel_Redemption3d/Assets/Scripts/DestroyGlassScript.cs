using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGlassScript : MonoBehaviour
{
    public GameObject Glass;


    private void OnTriggerEnter(Collider other)
    {
        var target = other.gameObject;
        switch (target.tag)
        {
            case "Bullet":
                // enable cloning
                Destroy(gameObject);
                break;
        }
    }

   
}
