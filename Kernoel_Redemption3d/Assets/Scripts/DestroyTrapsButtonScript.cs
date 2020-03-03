using System.Collections.Generic;
using UnityEngine;

public class DestroyTrapsButtonScript : MonoBehaviour
{
    // the array with all the traps
    public List<GameObject> traps = new List<GameObject>();
    public bool isActivated = true;
    private bool _isNearButton;
    private GameObject _button;

    private void Start()
    {
        _button = transform.Find("Button").gameObject;
        
        SetButtonColor();
        UpdateTraps();
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.E) || !_isNearButton) return;
        
        // update the state
        isActivated = !isActivated;
        
        SetButtonColor();
        UpdateTraps();
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

    // update the button color
    private void SetButtonColor()
    {
        if (!_button) return;

        var color = isActivated ? Color.green : Color.red;
        var buttonLight = _button.GetComponentInChildren<Light>();
        if (buttonLight) buttonLight.color = color;
    }
    
    // enable/disable the traps
    public void UpdateTraps()
    {
        if (isActivated) foreach(var trap in traps) trap.SetActive(true);
        if (!isActivated) foreach(var trap in traps) trap.SetActive(false);
    }
}