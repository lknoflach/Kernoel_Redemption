using UnityEngine;

public class CameraScript : MonoBehaviour
{
    //Variables 
    public Transform player;
    public float smooth = 0.3f;

    public float height;

    private Vector3 velocity = Vector3.zero;
    //Methods


    // Update is called once per frame
    private void Update()
    {
        if (player != null)
        {
            var pos = new Vector3();
            var position = player.position;
            pos.x = position.x;
            pos.z = position.z - 7f;
            pos.y = position.y + height;
            transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smooth);
        }
    }
}