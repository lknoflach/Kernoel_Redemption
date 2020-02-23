using UnityEngine;

public class BallRollingScript : MonoBehaviour
{
    public float speed = 2;

    private Rigidbody _rigidBody;

    // Start is called before the first frame update
    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");
        var movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        _rigidBody.AddForce(movement * speed);
    }
}