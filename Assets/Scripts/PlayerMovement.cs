using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight1 = 3f;
    public float jumpHeight2 = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask GroundMask;

    Vector3 velocity;
    bool isGrounded;
    bool doubled;

    private void Start()
    {
        doubled = false;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, GroundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            doubled = false;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump"))
        {
            if(isGrounded)
            {
                velocity.y += Mathf.Sqrt(jumpHeight1 * -2 * gravity);
            }
            else if(doubled == false)
            {
                velocity.y += Mathf.Sqrt(jumpHeight2 * -2 * gravity);
                doubled = true;
            }
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}