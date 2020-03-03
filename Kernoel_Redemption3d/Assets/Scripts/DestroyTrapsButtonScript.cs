using System.Collections.Generic;
using UnityEngine;

public class DestroyTrapsButtonScript : MonoBehaviour
{
    // the array with all the traps
    public List<GameObject> traps = new List<GameObject>();
    private bool _isNearButton;
    public bool isActivated = true;
    private GameObject _button;

    private void Start()
    {
        _button = transform.Find("Button").gameObject;
        SetButtonColor();
    }

    public void DisableTraps()
    {
        foreach(var trap in traps) trap.SetActive(false);
        }
    
    public void EnableTraps()
    {
        foreach(var trap in traps) trap.SetActive(true);
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

    private void SetButtonColor()
    {
        if (!_button) return;

        var color = isActivated ? Color.green : Color.red;
        var buttonLight = _button.GetComponentInChildren<Light>();
        if (buttonLight) buttonLight.color = color;
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.E) || !_isNearButton) return;
        
        // update the state
        isActivated = !isActivated;
        SetButtonColor();

        // enable/disable the traps
        if (isActivated) EnableTraps();
        if (!isActivated) DisableTraps();
    }
}