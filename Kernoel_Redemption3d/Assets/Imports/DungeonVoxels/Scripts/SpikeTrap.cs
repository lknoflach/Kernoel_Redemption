using UnityEngine;
using System.Collections;

public class SpikeTrap : MonoBehaviour
{
    public Transform spike;
    public string playerTag = "Player";
    public float activeDuration = 2f;
    public float inactiveDuration = 2f;
    public bool startActive = true;
    public bool randomize;
    private bool _isActive;

    private void Start()
    {
        if (startActive)
        {
            SetActive();
        }
        else
        {
            SetInactive();
        }
    }

    private void SetActive()
    {
        var duration = activeDuration;
        _isActive = true;
        spike.gameObject.SetActive(true);
        if (randomize)
        {
            duration = Random.Range(activeDuration * .25f, activeDuration * 1.25f);
        }

        Invoke(nameof(SetInactive), duration);
    }

    private void SetInactive()
    {
        var duration = activeDuration;
        _isActive = false;
        spike.gameObject.SetActive(false);
        if (randomize)
        {
            duration = Random.Range(inactiveDuration * .25f, inactiveDuration * 1.25f);
        }

        Invoke(nameof(SetActive), duration);
    }

    public bool GetStatus()
    {
        return _isActive;
    }
}