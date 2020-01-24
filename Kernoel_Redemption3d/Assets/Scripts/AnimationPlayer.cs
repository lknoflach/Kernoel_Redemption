using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
     Animator animator;
    private CharacterMovement characterMovement;
    private GameObject player;

int runStateHash = Animator.StringToHash("StartRunning");
int standStateHash = Animator.StringToHash("StopRunning");

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerHans");
        //get CharacterMovementScript to check if Player is moving
        if (player) characterMovement = player.GetComponent<CharacterMovement>();
        // Get the animator
        if (player) animator = GetComponent<Animator>();

        if(player) Debug.Log("found player");
    }

    // Update is called once per frame
    void Update()
    {
        if(characterMovement.player_is_moving)
        {
          //  Debug.Log("AnimationPlayer->Update() --- player_is_moving = " + characterMovement.player_is_moving);
            animator.SetTrigger(runStateHash);
        }
        else
        {
           // Debug.Log("player is standing");
            animator.SetTrigger(standStateHash);
        }
    }
}
