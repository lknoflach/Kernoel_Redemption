using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRollingScript : MonoBehaviour
{
    public float speed = 2;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertival = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertival);
        rb.AddForce(movement * speed);
    }
}
