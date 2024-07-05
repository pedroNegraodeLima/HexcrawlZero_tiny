using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCtrl : MonoBehaviour
{
    Rigidbody playerRB;
    InputManager inputManager;

    public float jumpForce = 5f; // Jump force variable
    public LayerMask groundLayer; // Ground layer variable
    public Transform groundCheck; // Ground check variable
    public float groundCheckRadius = 0.2f; // Ground check radius variable
    public float fallDrag = 2f; // Drag applied when falling
    private bool isGrounded; // isGrounded variable
    private float originalDrag; // Original drag value

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
        inputManager = GetComponent<InputManager>();
        originalDrag = playerRB.drag; // Store the original drag value
    }

    private void Update()
    {
        HandleJump();
    }

    private void HandleJump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded && inputManager.testJumpButton)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerRB.drag = originalDrag; // Reset drag when jumping
        }

        // Apply additional drag when falling
        if (playerRB.velocity.y < 0)
        {
            playerRB.drag = fallDrag;
        }
        else
        {
            playerRB.drag = originalDrag; // Reset drag when not falling
        }
    }
}
