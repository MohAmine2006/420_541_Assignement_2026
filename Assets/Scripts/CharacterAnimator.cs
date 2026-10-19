
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    private Animator animator;
    private CharacterMovement movement;
    private Rigidbody rb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<CharacterMovement>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Send the player's speed to the Animator
        animator.SetFloat("CharacterSpeed", rb.linearVelocity.magnitude);

        // Update the grounded state
        bool grounded = movement.IsGrounded;
        animator.SetBool("IsGrounded", grounded);

        if (grounded)
        {
            // Clear any unused double-jump animation trigger
            animator.ResetTrigger("doDoubleJump");
        }
        else
        {
            // Double jump only if the player is in the air
            if (Input.GetButtonDown("Jump") && movement.TryDoubleJump())
            {
                animator.SetTrigger("doDoubleJump");
            }
        }
    }
}
