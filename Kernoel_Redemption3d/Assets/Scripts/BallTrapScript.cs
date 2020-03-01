using System.Collections.Generic;
using UnityEngine;

public class BallTrapScript : MonoBehaviour
{
    // the array with all the traps
    public List<GameObject> traps = new List<GameObject>();
    private bool _isNearButton;

    public void SwitchTraps()
    {
        foreach (var trap in traps)
        {
            Destroy(trap);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var target = other.gameObject;
        switch (target.tag)
        {
            case "Ball":
                // enable cloning
                _isNearButton = true;
                break;
        }
    }



    private void Update()
    {
        if (_isNearButton)
        {
            // destroy the traps
            SwitchTraps();
        }
    }
}