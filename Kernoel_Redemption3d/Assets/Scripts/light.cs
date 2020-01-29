using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light : MonoBehaviour
{
    Light testlight;
    public float minWaitTime;
    public float maxWaitTime;

    void Start()
    {
        testlight = GetComponent<Light>();
        StartCoroutine(Flashing());
    }

    // Update is called once per frame
    IEnumerator Flashing()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
        }
    }
}
