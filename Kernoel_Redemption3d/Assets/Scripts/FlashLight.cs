using System.Collections;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    Light _testlight;
    public float minWaitTime;
    public float maxWaitTime;

    private void Start()
    {
        _testlight = GetComponent<Light>();
        StartCoroutine(Flashing());
    }

    // Update is called once per frame
    private IEnumerator Flashing()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
        }
    }
}