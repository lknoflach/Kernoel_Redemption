using UnityEngine;

public class DestroyOnExplosion : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Trap"))
        {
            Destroy(gameObject);
        }
    }
}