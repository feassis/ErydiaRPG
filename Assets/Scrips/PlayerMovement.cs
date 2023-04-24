using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float deaceleration;
    [SerializeField] private float groundDrag;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMultiplier;
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform characterCenter;
    [SerializeField] private Rigidbody rb;

    [Header("GroundCheck")]
    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask whatIsGround;

    [Header("Keybinds")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;

    [SerializeField] bool isGrounded;
    bool isReadyToJump = true;

    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;

    private void Update()
    {
        isGrounded = Physics.Raycast(characterCenter.transform.position, Vector3.down,
            playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();
        SpeedControl();

        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }

    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(jumpKey) && isReadyToJump && isGrounded)
        {
            isReadyToJump = false;
            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * airMultiplier * 10f, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }

        if (horizontalInput == 0 && verticalInput == 0 
            && rb.velocity != Vector3.zero && isGrounded)
        {
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, Time.deltaTime * deaceleration);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        isReadyToJump = true;
    }
}
