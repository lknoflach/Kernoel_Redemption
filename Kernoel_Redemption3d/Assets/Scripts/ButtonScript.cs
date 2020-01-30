using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{

    public GameObject Trap1;
    public GameObject Trap2;
    public GameObject Trap3;
    public GameObject Trap4;
    private bool isNearButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void trap()
    {
        Destroy(Trap1);
        Destroy(Trap2);
        Destroy(Trap3);
        Destroy(Trap4);
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
            trap();
        }
    }
}
