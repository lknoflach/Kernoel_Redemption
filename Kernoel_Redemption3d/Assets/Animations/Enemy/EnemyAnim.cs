using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnim : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    private EnemyScript enemscript;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Enemy");
        //get CharacterMovementScript to check if Player is moving
        enemscript = player.GetComponent<EnemyScript>();
        // Get the animator
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemscript.isShooting == true)
        {
            animator.SetBool("isShooting", true);
        }
        else
        {
            animator.SetBool("isShooting", false);
        }
    }
}
