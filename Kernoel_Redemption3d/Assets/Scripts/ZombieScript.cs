using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ZombieScript : MonoBehaviour
{
    /** MOVEMENT STUFF **/
    public bool isArrivedAtPlayer = true;

    //the values are a bit guessed
    public float movementSpeed = 20;
    public bool moveOnlyOnSight = true;
    public float fieldOfViewDegrees = 90.0f;
    public float visibilityDistance = 200000.0f;
    
    //if zombie is visible on the screen so if it gets in screen 
    bool isVisible = false;

    //if zombie has seen the player 
    bool hasSeenPlayer = false; 

    /** PLAYER STUFF **/
    private GameObject player;

    private CharacterMovement playerMovement;

    private void Start()
    {
        player = GameObject.Find("PlayerHans");
        if (player) playerMovement = player.GetComponent<CharacterMovement>();
    }

    void OnBecameVisible()
    {
       Debug.Log("Zombie is visible");
        isVisible = true;
    }

    private void OnCollisionEnter(Collision col)
    { 
            if (!isArrivedAtPlayer)
            {
                switch ( col.gameObject.tag)
                {
                    case "Clone":
                        //maybe to something with the clone
                        //var cloneScript =  col.gameObject.GetComponent<CloneScript>();
                        //if (cloneScript && cloneScript.isArrivedAtPlayer)
                        //{
                        Debug.Log("Zombie is arrived at other Clone");
                            //should zombies turn clones into zombies ?? 
                        //    isArrivedAtPlayer = true;
                        //}
                        break;

                    case "Player":
                        Debug.Log("Zombie is arrived at Player");
                        isArrivedAtPlayer = true;
                        break;
                }
            }
    }


  private bool CanSeePlayer()
    {
        if (player)
        {
            // RaycastHit hit;
            var rayDirection = player.transform.position - transform.position;
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
        }

        return false;
    }

    private void MoveToPlayer()
    {
        if (player)
        {
            transform.LookAt(player.transform.position);
            transform.position += Time.deltaTime * movementSpeed * transform.forward;
        }
    }

    private void Update()
    {
        ////for some reason the OnBecameVisible funtction does not work yet     
          // if (isVisible){  
            if (hasSeenPlayer)
            {
                MoveToPlayer();
            }
            else
            {
                hasSeenPlayer = CanSeePlayer();
            }
      //  }
    }
    
}