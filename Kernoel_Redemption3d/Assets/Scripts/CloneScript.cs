using System;
using UnityEngine;

public class CloneScript : MonoBehaviour
{
    /** MOVEMENT STUFF **/
    public bool isArrivedAtPlayer;

    public float currentMovementSpeed = 10f;
    public float movementSpeed = 10f;

    /** PLAYER STUFF **/
    private GameObject player;

    private CharacterMovement characterMovement;

    private void Start()
    {
        //get Player to get the CharacterMovementScript
        player = GameObject.Find("PlayerHans");
        //get CharacterMovementScript to check if Player is moving
        if (player) characterMovement = player.GetComponent<CharacterMovement>();
        currentMovementSpeed = movementSpeed;
    }

    private void checkIfArivedAtPlayer(Collision other)
    {
       
        if (!isArrivedAtPlayer)
        {
            var target = other.gameObject;
            switch (target.tag)
            {
                case "Clone":
                    var cloneScript = target.GetComponent<CloneScript>();
                    if (cloneScript && cloneScript.isArrivedAtPlayer)
                    {
                        // Debug.Log("Clone is arrived at other Clone");
                        isArrivedAtPlayer = true;
                    }

                    break;

                case "Player":
                    // Debug.Log("Clone is arrived at Player");
                    isArrivedAtPlayer = true;
                    break;
            }
        }

        if (isArrivedAtPlayer) currentMovementSpeed = 0f;
    }

    private void OnCollisionEnter(Collision other)
    {
      checkIfArivedAtPlayer(other);
    }

    
    private void OnCollisionStay(Collision other)
    {
        checkIfArivedAtPlayer(other);
    }

    private void Update()
    {
        if (player && characterMovement)
        {
            // Debug.Log(playerScript.moveInput);
            //ask if player moves
            if (characterMovement.player_is_moving)
            {
                isArrivedAtPlayer = false;
                currentMovementSpeed = movementSpeed;
            }

            if (!isArrivedAtPlayer)
            {
                transform.LookAt(player.transform.position);
                // transform.position += Time.deltaTime * currentMovementSpeed * transform.forward;
                transform.position += 0.02f * currentMovementSpeed * transform.forward;
            }
        }
    }
}