﻿using UnityEngine;

/// <summary>
/// This script moves the character controller forward and sideways based on the arrow keys. It also jumps when
/// pressing space. Make sure to attach a character controller to the same game object. It is recommended that you
/// make only one call to Move or SimpleMove per frame.
/// </summary>
public class CharacterMovement : MonoBehaviour
{
    CharacterController characterController;

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private Vector3 moveDirection = Vector3.zero;
    public Vector3 move = Vector3.zero;

    Animator anim;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (characterController.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        move = moveDirection * Time.deltaTime;

        if (moveDirection.y == 0 && moveDirection.y == 0)
        {
            anim.SetInteger("condition", 0); 
        }
        else
        {
            anim.SetInteger("condition", 1);
        }
        characterController.Move(move);
    }
}