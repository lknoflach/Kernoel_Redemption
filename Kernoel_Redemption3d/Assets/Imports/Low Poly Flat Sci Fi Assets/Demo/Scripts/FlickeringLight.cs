using System.Collections;
using UnityEngine;

//	This script handles flickering for lights
//	It is expected the light contains a true Unity Light source so will turn it on and off and will change the model texture for better effect
[RequireComponent(typeof(MeshRenderer))]
public class FlickeringLight : MonoBehaviour
{
    private Light _light;
    [SerializeField] private float minWaitTime = 0.1f;
    [SerializeField] private float maxWaitTime = 0.5f;
    [SerializeField] private int materialIdx;

    [SerializeField] private Material onMaterial;
    [SerializeField] private Material offMaterial;

    private MeshRenderer _meshRenderer;
    private Material[] _materials;

    // Use this for initialization
    private void Start()
    {
        _light = GetComponentInChildren<Light>();
        if (_light != null)
        {
            StartCoroutine(FlickerLight());
        }

        _meshRenderer = GetComponent<MeshRenderer>();
        _materials = _meshRenderer.materials;
    }

    //	Turn on and off the light
    private IEnumerator FlickerLight()
    {
        // TODO Iterator never ends
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            _light.enabled = !_light.enabled;

            //	Updates the model material based on the real light status
            if (_light.enabled)
            {
                _materials[materialIdx] = onMaterial;
            }
            else
            {
                _materials[materialIdx] = offMaterial;
            }

            _meshRenderer.materials = _materials;
        }
    }
}