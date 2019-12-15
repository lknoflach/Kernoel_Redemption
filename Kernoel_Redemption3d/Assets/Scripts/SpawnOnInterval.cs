using UnityEngine;

public class SpawnOnInterval : MonoBehaviour
{
    public GameObject obj;
    public float interval;
    public float firstSpawnAfter;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnGameObject), firstSpawnAfter, interval);
    }

    private void SpawnGameObject()
    {
        var instance = Instantiate(obj);
        // Activate the GameObject if it is disabled
        if (!instance.gameObject.activeSelf) instance.gameObject.SetActive(true);
    }
}