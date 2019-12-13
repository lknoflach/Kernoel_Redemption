using UnityEngine;

public class SpawnOnInterval : MonoBehaviour
{
    public Rigidbody obj;
    public float interval;
    public float firstSpawnAfter;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnBim), firstSpawnAfter, interval);
    }

    private void SpawnBim()
    {
        var instance = Instantiate(obj);
        instance.velocity = Random.insideUnitSphere * 5;
    }
}