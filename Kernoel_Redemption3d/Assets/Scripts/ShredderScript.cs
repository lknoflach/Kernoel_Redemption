using UnityEngine;
using UnityEngine.Serialization;

public class ShredderScript : MonoBehaviour
{
    [FormerlySerializedAs("x_speed")] public float xSpeed = 5f;
    [FormerlySerializedAs("y_speed")] public float ySpeed = 5f;
    [FormerlySerializedAs("z_speed")] public float zSpeed = 5f;

    private void Update()
    {
        transform.Rotate(xSpeed, ySpeed, zSpeed);
        // Rotation amount of rotation per frame
    }
}