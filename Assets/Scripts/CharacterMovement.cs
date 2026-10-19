
using UnityEngine;

// Moves the player with a Rigidbody:
// A/D or Left/Right arrows to ROTATE, W/S or Up/Down to move
// Forward/Backward, Space to jump.

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float turnSpeed = 150f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float doubleJumpForce = 5f;

    [Header("Ground Check Settings")]
    [SerializeField] private float groundDistance = 0.5f;
    [SerializeField] private LayerMask groundMask;

    private Rigidbody rb;
    private Vector3 moveDirection;
    private float turnInput;
    private bool isGrounded;
    private bool jumpRequested = false;
    private bool doubleJumpRequested = false;
    private bool hasDoubleJumped = false;
    private JumpBoostPowerUp jumpBoost;

    public bool IsGrounded => isGrounded;
    public float SpeedMultiplier { get; set; } = 1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        jumpBoost = GetComponent<JumpBoostPowerUp>();
    }

    private void Update()
    {
        // 1. Ground Check
        isGrounded = Physics.Raycast(
            transform.position + transform.up * groundDistance / 2,
            -transform.up,
            groundDistance,
            groundMask
        );

        if ( isGrounded ) {
            hasDoubleJumped = false; // Reset double jump when grounded
        }

        // 2. Read Inputs
        turnInput = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        // Rotate the player transform in Update for responsive turning visuals
        transform.Rotate(
            0f,
            turnInput * turnSpeed * Time.deltaTime,
            0f
        );

        // Calculate forward movement relative to current facing direction
        moveDirection = transform.forward * moveZ;

        // 3. Handle Jump Input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpRequested = true;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();

        if (jumpRequested)
        {
            Jump(jumpForce);
            jumpRequested = false;
        }

        if (doubleJumpRequested)
        {
            Jump(doubleJumpForce);
            doubleJumpRequested = false;
        }
    }

    private void MovePlayer()
    {
        // Sprinting: If Left Shift or Right Shift is held, double the movement speed
        float currentSpeed = moveSpeed;

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            currentSpeed = moveSpeed * 2f;
        }

        // Calculate velocity based on current forward vector
        Vector3 targetVelocity = moveDirection * currentSpeed * SpeedMultiplier;

        // Apply movement while preserving vertical velocity (gravity/jumping)
        rb.linearVelocity = new Vector3(
            targetVelocity.x,
            rb.linearVelocity.y,
            targetVelocity.z
        );
    }

    private void Jump(float force)
    {
        rb.linearVelocity = new Vector3(
            rb.linearVelocity.x,
            0f,
            rb.linearVelocity.z
        );

        rb.AddForce(
            Vector3.up * force,
            ForceMode.VelocityChange
        );
    }

    public bool TryDoubleJump()
    {
        if ( jumpBoost == null || jumpBoost.JumpTimeLeft <= 0f)
        {
            return false;
        }

        if (isGrounded || hasDoubleJumped)
        {
            return false;
        }

        doubleJumpRequested = true;
        hasDoubleJumped = true;

        return true;
    }

}
