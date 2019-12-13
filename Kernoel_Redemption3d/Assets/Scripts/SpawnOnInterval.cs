using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnInterval : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody obj; 
    public float interval; 
    public float firstSpawnAfter;

    void Start()
    {
        InvokeRepeating("SpawnBim", firstSpawnAfter, interval);
    }

    // Update is called once per frame
        void SpawnBim()
    {
        Rigidbody instance = Instantiate(obj);

        instance.velocity = Random.insideUnitSphere * 5;
    }
}
