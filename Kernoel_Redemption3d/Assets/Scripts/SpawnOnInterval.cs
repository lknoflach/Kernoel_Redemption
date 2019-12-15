using UnityEngine;

public class SpawnOnInterval : MonoBehaviour
{
    public GameObject obj;
    public float interval;
    public float firstSpawnAfter;
    /// <summary>
    /// Spawn position of the GameObject.
    /// </summary>
    public Transform spawnPoint;
    /// <summary>
    /// Total amount of spawns. If set to zero then Infinite spawns will occur.
    /// </summary>
    public int spawnCount;
    /// <summary>
    /// Total amount of spawns occured. Spawning will be stopped, if currentSpawnCount reaches spawnCount. 
    /// </summary>
    private int currentSpawnCount;
    /// <summary>
    /// Is set to true spawnCount is initialized with zero.
    /// </summary>
    private bool isInfinite;

    private void Start()
    {
        if (spawnCount == 0) isInfinite = true;
        InvokeRepeating(nameof(SpawnGameObject), firstSpawnAfter, interval);
    }

    private void SpawnGameObject()
    {
        // Spawn if isInfinite or if we haven't spawned the full spawnCount.
        if (isInfinite || (currentSpawnCount < spawnCount))
        {
            var instance = Instantiate(obj, spawnPoint);
            // Activate the GameObject if it is disabled
            if (!instance.gameObject.activeSelf) instance.gameObject.SetActive(true);

            currentSpawnCount++;
        }
    }
}