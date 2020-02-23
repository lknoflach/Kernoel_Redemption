using UnityEngine;

public class CameraScript : MonoBehaviour
{
    //Variables 
    public Transform player;
    public float smooth = 0.3f;

    public float height;

    public float ofSetX = -9f;

    private Vector3 _velocity = Vector3.zero;

    private void Update()
    {
        if (!player) return;

        var pos = new Vector3();
        var position = player.position;
        pos.x = position.x;
        pos.z = position.z + ofSetX;
        pos.y = position.y + height;
        transform.position = Vector3.SmoothDamp(transform.position, pos, ref _velocity, smooth);
    }
}