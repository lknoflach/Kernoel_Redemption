using UnityEngine;

public class DestroyGlassScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var target = other.gameObject;
        switch (target.tag)
        {
            case "Bullet":
                Destroy(gameObject);
                break;
        }
    }
}