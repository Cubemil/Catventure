using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed = 8f;
    public float jumpForce = 5.0f;
    public float rotationSpeed = 0f;
    private Rigidbody rb;
    private bool isOnGround = true;
    public GameObject cat;
    private Animator animator;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
            // get player input
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
        
            // move player horizontally and vertically 
            Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);

            if (movementDirection != Vector3.zero) // ensure movement direction is not zero to avoid normalization issues
            {
                animator.SetBool("isWalking", true);
            
                movementDirection.Normalize(); // normalize the vector to ensure consistent speed
                float movementFactor = speed * Time.deltaTime; // combine scalars first
                transform.Translate(movementDirection * movementFactor, Space.World); // Space.World -> movement is adjusted to game world 

                // rotate player to movementDirection
                Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up); // Quaternion = type to store rotations
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        
            // let player run
            if (movementDirection != Vector3.zero && Input.GetButtonDown("Run"))
            {
                animator.SetBool("isRunning", true);
                speed = 16.0f;
            }
            else if (movementDirection != Vector3.zero && Input.GetButtonUp("Run"))
            {
                animator.SetBool("isRunning", false);
                speed = 8.0f;
            }
        
            // let player jump
            if (Input.GetButtonDown("Jump") && isOnGround)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isOnGround = false;
            }
        
    }
    
    private void OnCollisionEnter(Collision collision) // check collision of player and ground
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }
}