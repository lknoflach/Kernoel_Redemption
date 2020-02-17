using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkSkript : MonoBehaviour
{
    GameObject text;
    public bool isNearButton;
    // Start is called before the first frame update
    void Start()
    {
        isNearButton = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        var target = other.gameObject;
        switch (target.tag)
        {
            case "Player":
                // enable cloning
                isNearButton = true;
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var target = other.gameObject;
        switch (target.tag)
        {
            case "Player":
                // enable cloning
                isNearButton = false;
                break;
        }
    }


    // Update is called once per frame
    void Update()
    {
        //cloning Button
        if (Input.GetKeyDown(KeyCode.E) && isNearButton)
        {
       
        }
     
    }
}
