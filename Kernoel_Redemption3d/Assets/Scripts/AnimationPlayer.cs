using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{

    private GameObject player;
    private CharacterMovement characterMovement;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
         player = GameObject.Find("PlayerHans");
        //get CharacterMovementScript to check if Player is moving
        if (player) characterMovement = player.GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {

        if(characterMovement.player_is_moving)
        {
            Debug.Log("Player Hans is moving time for animation");
            //anim.SetInteger("condition", 0);
        }
        else
        {
            Debug.Log("Player Hans is not moving no animation");
            //anim.SetInteger("condition", 1);
        }

        //anim.SetInteger("condition", 1);
    }
}
