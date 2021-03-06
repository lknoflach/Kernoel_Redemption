﻿using UnityEngine;

public class EndLevelMessage : MonoBehaviour
{
    // private bool _showName;
    public string nextLevelName;

    private void OnTriggerEnter(Collider other)
    {
        // sets display message to true an displays the message
        if (!other.gameObject.CompareTag("Player")) return;

        GameManager.Instance.FinishLevel(nextLevelName);
    }

    /*private void OnGUI()
    {
        if (_showName)
            GUI.Label(new Rect(400, 200, 2000, 2000), "You finished the level! :)");
    }*/
}