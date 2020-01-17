using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    private Animator animator;
    private CharacterMovement characterMovement;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
         player = GameObject.Find("PlayerHans");
        //get CharacterMovementScript to check if Player is moving
        if (player) characterMovement = player.GetComponent<CharacterMovement>();
        // Get the animator
        if (player) animator = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(characterMovement.player_is_moving)
        {
            Debug.Log("AnimationPlayer->Update() --- player_is_moving = " + characterMovement.player_is_moving);
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }
}
