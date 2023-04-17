using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    [SerializeField] private float speed = 5;
    [SerializeField] private float runSpeed = 10;
    [SerializeField] private float gravity = -9.18f;
    [SerializeField] private float jumpHeight = 3f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float rotationSpeed = 10f;

    Vector3 velocity;
    bool isGrounded;
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float currentSpeed;

        if (Input.GetKey("left shift") && isGrounded)
        {
            currentSpeed = runSpeed;
        }
        else
        {
            currentSpeed = speed;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if(x == 0 && z == 0)
        {
            return;
        }

        var movementDirection = new Vector3(x, 0, z);

        movementDirection.Normalize();

        controller.Move(movementDirection * currentSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Lerp(Quaternion.LookRotation(movementDirection),
            transform.rotation, rotationSpeed * Time.deltaTime);
    }
}