using UnityEngine;
using System.Collections;
public class CloneController : MonoBehaviour
{



     public GameObject Player;
     public float movementSpeed = 10;
     public int live = 50;
     public bool arivedAtPlayer = false;
        private PlayerScript playerScript; 
    private CloneController clone;
 
     void Start()

    {
        playerScript = Player.GetComponent<PlayerScript>();
    }

 void OnCollisionEnter(Collision col)
     {  
         if(!arivedAtPlayer){
        if(col.gameObject.tag == "Clone"){
               clone = col.gameObject.GetComponent<CloneController>();
               if(clone.arivedAtPlayer){   
                    Debug.Log("colider clone");
                arivedAtPlayer = true;
                   
               }
            }
        }
          
         
         if(col.gameObject.name == "PlayerCube"){
                arivedAtPlayer = true;
                    Debug.Log("Test");
         }


        //collisionCount++;
    }

    /* void OnCollisionExit(Collision col)
     {

         if(col.gameObject == Player ){
           arivedAtPlayer = false;
             playerArrClone = false;

            // GetComponent<Rigidbody>().isKinematic = false;
         }
     
     if(clone2 != null){
        if( col.gameObject.tag == "Clone" && col.gameObject.name == clone2.name){
          //  Debug.Log("colider exit");
            arivedAtPlayer = false;
            clone2 = null;
            }
         }
        // Debug.Log("col exit");
         //collisionCount--;
     }
  */

     void Update()
     {
       if(live <= 0){
         //die 
         Destroy(gameObject);
       }
       
      // Debug.Log(playerScript.moveInput);
     if( !Mathf.Approximately(playerScript.moveInput.x, 0.0f) ||  !Mathf.Approximately(playerScript.moveInput.y, 0.0f) ||  !Mathf.Approximately(playerScript.moveInput.z, 0.0f)){
             arivedAtPlayer = false;
            
         } 
         
         if(arivedAtPlayer==false){
         transform.LookAt(Player.transform);
         transform.position += transform.forward * movementSpeed * Time.deltaTime;

        }

        if (arivedAtPlayer == false)
        {
            transform.LookAt(Player.transform);
            transform.position += transform.forward * movementSpeed * Time.deltaTime;
        }

    }


}
