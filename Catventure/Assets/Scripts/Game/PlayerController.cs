using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed = 5.0f;
    public float force = 5.0f;
    public float rotationSpeed = 0f;
    private Rigidbody rb;
    private bool isOnGround = true;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // get player input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        // move player horizontally and vertically 
        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World); // Space.World -> movement is adjusted to game world 

        // rotate player to movementDirection
        if (movementDirection != Vector3.zero) // if player moves -> there is a horizontal and vertical input
        {
            //transform.forward = movementDirection; // set transform.forward to movementDirection
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up); // Quaternion = type to store rotations
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        
        // let player jump
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            rb.AddForce(Vector3.up * force, ForceMode.Impulse);
            isOnGround = false;
        }
    }

    // check collision of player and ground
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }
}