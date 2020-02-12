using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    private ZombieScript zombiescript;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Zombie");
        //get CharacterMovementScript to check if Player is moving
        zombiescript = player.GetComponent<ZombieScript>();
        // Get the animator
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (zombiescript.isMoving == false)
        {
            animator.SetBool("Zwalk", false);
        }
        else
        {
            animator.SetBool("Zwalk", true);
        }
    }
}
