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
        characterMovement = player.GetComponent<CharacterMovement>();
        // Get the animator
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(characterMovement.player_is_moving)
        {
            animator.SetBool("player_is_moving", true);
        }
        else
        {
            animator.SetBool("player_is_moving", false);
        }
    }
}
