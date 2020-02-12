using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneAnimScript : MonoBehaviour
{
    private Animator animator;
    private CloneScript cloneScript;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Clone");
        //get CharacterMovementScript to check if Player is moving
        cloneScript = player.GetComponent<CloneScript>();
        // Get the animator
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cloneScript.isArrivedAtPlayer == false)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}