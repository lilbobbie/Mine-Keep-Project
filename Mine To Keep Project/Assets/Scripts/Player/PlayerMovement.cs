using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementState
{
    Walking,
    Sprinting,
}

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float sprintSpeed;

    [SerializeField]
    private float groundDrag;


    [Header("Keybinds")]
    public KeyCode sprintKey = KeyCode.LeftShift;


    [Header("Ground Check")]
    [SerializeField]
    private LayerMask whatIsGround;


    [Header("Slope Handling")]
    [SerializeField]
    private float maxSlopeAngle;
    private RaycastHit slopeHit;
    [SerializeField]
    private float playerHeight;


    [SerializeField]
    private Transform orientation;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;

    private Rigidbody rb;


    public MovementState movementState { get; private set; }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        GetInput();
        ControlSpeed();
        HandleState();
        HandleDrag();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void HandleState()
    {
        // Sprinting
        if (Input.GetKey(sprintKey))
        {
            movementState = MovementState.Sprinting;
            moveSpeed = sprintSpeed;
        }

        // Walking
        else
        {
            movementState = MovementState.Walking;
            moveSpeed = walkSpeed;
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (IsOnSlope())
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);
        }

        else
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }

        if (!IsGrounded())
        {
            rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        //rb.useGravity = !IsOnSlope();
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
    }

    private void HandleDrag()
    {
        rb.drag = groundDrag;
        return;
    }

    private void ControlSpeed()
    {
        if (IsOnSlope())
        {
            if (rb.velocity.magnitude > moveSpeed)
            {
                rb.velocity = rb.velocity.normalized * moveSpeed;
            }
        }

        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }

    private bool IsOnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.6f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }
}
