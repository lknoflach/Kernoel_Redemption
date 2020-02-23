using System.Collections.Generic;
using UnityEngine;

public class DestroyTrapsButtonScript : MonoBehaviour
{
    // the array with all the traps
    public List<GameObject> traps = new List<GameObject>();
    private bool _isNearButton;

    public void SwitchTraps()
    {
        foreach(var trap in traps)
        {
            trap.SetActive(!trap.activeSelf);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var target = other.gameObject;
        switch (target.tag)
        {
            case "Player":
                // enable cloning
                _isNearButton = true;
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var target = other.gameObject;
        switch (target.tag)
        {
            case "Player":
                _isNearButton = false;
                break;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _isNearButton)
        {
            // destroy the traps
            SwitchTraps();
        }
    }
}