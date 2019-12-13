using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelMessage : MonoBehaviour
{
  
    private bool showName = false; 


    void OnTriggerEnter(Collider other){
        //sets display message to true an displays the message
        if(other.gameObject.CompareTag("Player")){
            //if you want to load a new scene when you reached the goal to it here
            Debug.Log("END");
            showName = true;
        }
    }
         void OnGUI()
     {
         if(showName)
             GUI.Label(new Rect(400,200,2000,2000), "You finished the level! :)");
     }

}
