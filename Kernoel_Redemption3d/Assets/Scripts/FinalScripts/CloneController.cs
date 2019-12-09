using UnityEngine;
using UnityEngine.Serialization;

public class CloneController : MonoBehaviour
{
    /** CHARACTER STUFF **/
    [FormerlySerializedAs("live")] public int health = 50;
    private CloneController cloneController;
    
    /** MOVEMENT STUFF **/
    [FormerlySerializedAs("arivedAtPlayer")] public bool isArrivedAtPlayer = false;
    public float movementSpeed = 10;


    /** PLAYER STUFF **/
    private GameObject player;
    private PlayerScript playerScript;

    private void Start()

    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerScript>();
    }

    private void OnCollisionEnter(Collision col)
    {
        if (!isArrivedAtPlayer)
        {
            if (col.gameObject.CompareTag("Clone"))
            {
                cloneController = col.gameObject.GetComponent<CloneController>();
                if (cloneController.isArrivedAtPlayer)
                {
                    Debug.Log("collider clone");
                    isArrivedAtPlayer = true;
                }
            }
        }
        
        if (col.gameObject.tag == "Player")
        {
            isArrivedAtPlayer = true;
            Debug.Log("Clone is arrived at Player");
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
        if (health <= 0)
        {
            //die 
            Destroy(gameObject);
        }

        // Debug.Log(playerScript.moveInput);
        if (!Mathf.Approximately(playerScript.moveInput.x, 0.0f) ||
            !Mathf.Approximately(playerScript.moveInput.y, 0.0f) ||
            !Mathf.Approximately(playerScript.moveInput.z, 0.0f))
        {
            isArrivedAtPlayer = false;
        }

        if (isArrivedAtPlayer == false)
        {
            transform.LookAt(player.transform.position);
            transform.position += Time.deltaTime * movementSpeed * transform.forward;
        }
    }
}