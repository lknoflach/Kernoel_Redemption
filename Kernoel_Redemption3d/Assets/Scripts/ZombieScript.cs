using UnityEngine;
using UnityEngine.Serialization;

public class ZombieScript : MonoBehaviour
{
    /** CHARACTER STUFF **/
    public int health = 50;
    private CloneController cloneController;

    /** MOVEMENT STUFF **/
    public bool isArrivedAtPlayer = true;
    public float movementSpeed = 10;
    public bool moveOnlyOnSight = true;
    public float fieldOfViewDegrees = 90.0f;
    public float visibilityDistance = 200000.0f; 


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
        if(moveOnlyOnSight == false){
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
        }else
        {   
            Debug.Log(CanSeePlayer());
            if (health <= 0)
            {
                //die 
                Destroy(gameObject);
            }
            
            if(CanSeePlayer()){
                transform.LookAt(player.transform.position);
                transform.position += Time.deltaTime * movementSpeed * transform.forward;
            }
        }
    }
    
    protected bool CanSeePlayer()
    {
        RaycastHit hit;
        Vector3 rayDirection = player.transform.position - transform.position;
 
        if ((Vector3.Angle(rayDirection, transform.forward)) <= fieldOfViewDegrees * 0.5f)
        {
            //Debug.Log("test");
             // Detect if player is within the field of view
           // if (Physics.Raycast(transform.position, rayDirection, out hit, visibilityDistance))
           // {
                return true;
               // return (hit.transform.CompareTag("Player"));
           // }
        }
 
        return false;
    }
}