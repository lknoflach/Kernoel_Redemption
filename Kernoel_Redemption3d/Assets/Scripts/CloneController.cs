 using UnityEngine;
using System.Collections;
 public class CloneController : MonoBehaviour
 {

     public GameObject Player;
     public float movementSpeed = 4;
     public bool arivedAtPlayer = false;
        private PlayerScript playerScript; 
 
 
     void Start()
    {
      playerScript = Player.GetComponent<PlayerScript>();
    }
 
 void OnCollisionEnter(Collision col)
     {  
       //  if(!arivedAtPlayer){
        /* if(col.gameObject.tag == "Clone"){
               clone2 = col.gameObject.GetComponent<CloneController>();
               if(clone2.arivedAtPlayer && arivedAtPlayer == false){   
                   clone = clone2;             
                    Debug.Log("colider clone");
                arivedAtPlayer = true;
                   
               }
            }
  //  }
 */
         
         
         if(col.gameObject.name == "Player"){
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
       //Debug.Log(playerScript.move);
         if( !Mathf.Approximately(playerScript.moveSpeed, 0.0f)){
             arivedAtPlayer = false;
            
         } 
         
         if(arivedAtPlayer==false){
         transform.LookAt(Player.transform);
         transform.position += transform.forward * movementSpeed * Time.deltaTime;
        }
        
     }
    

 }
