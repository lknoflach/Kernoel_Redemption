using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HäckslerScript : MonoBehaviour
{
    public float x_speed = 5f;
    public float y_speed = 5f;
    public float z_speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(x_speed, y_speed, z_speed);
            // Rotation amout of raotation per frame


    }
}
