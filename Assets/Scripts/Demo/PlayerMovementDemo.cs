using UnityEngine;

/// <summary>
/// Demo script demonstrating player movement with CharacterController and root motion animation.
/// Handles movement, rotation, jumping, and animation state management.
/// </summary>
public class PlayerMovementDemo : MonoBehaviour
{
    [Header("Component References")]
    /// <summary>
    /// Reference to the Animator component for controlling animations.
    /// </summary>
    public Animator animator;
    
    /// <summary>
    /// Reference to the CharacterController component for movement and collision.
    /// </summary>
    public CharacterController characterController;
    
    [Header("Movement Settings")]
    /// <summary>
    /// The speed at which the player moves (used for animation blending).
    /// </summary>
    public float moveSpeed;
    
    /// <summary>
    /// Maximum rotation speed in degrees per second.
    /// </summary>
    public float rotationMaxDegree;
    
    /// <summary>
    /// The initial upward velocity when jumping.
    /// </summary>
    public float jumpHeight;

    /// <summary>
    /// Current vertical velocity/gravity value.
    /// </summary>
    private float yGravity;
    
    /// <summary>
    /// Flag indicating if the player is currently jumping.
    /// </summary>
    private bool isJumping;
    
    /// <summary>
    /// Flag indicating if the player is grounded.
    /// </summary>
    private bool isGrounded;

    /// <summary>
    /// Initializes component references.
    /// </summary>
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Handles player input, movement, jumping, rotation, and animation updates.
    /// </summary>
    void Update()
    {
        // Get input from horizontal and vertical axes
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction from input
        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);
        float inputMagnitude = moveDirection.magnitude;

        // Update animator with input magnitude for blend tree
        animator.SetFloat("InputMagnitude", inputMagnitude, 0.05f, Time.deltaTime);

        // Normalize direction vector
        moveDirection.Normalize();

        // Apply gravity
        yGravity += Physics.gravity.y * Time.deltaTime;

        // Handle grounded state and jumping
        if (characterController.isGrounded)
        {
            // Reset gravity when grounded
            yGravity = -0.5f;
            animator.SetBool("IsGrounded", true);
            isGrounded = true;
            animator.SetBool("IsJumping", false);
            isJumping = false;
            animator.SetBool("IsFalling", false);

            // Handle jump input
            if (Input.GetButtonDown("Jump"))
            {
                yGravity = jumpHeight;
                animator.SetBool("IsJumping", true);
                isJumping = true;
            }
        }
        else
        {
            // Update air state
            animator.SetBool("IsGrounded", false);
            isGrounded = false;

            // Detect falling state (after peak of jump or fast downward velocity)
            if((isJumping && yGravity < 0) || yGravity < -3.5f)
            {
                animator.SetBool("IsFalling", true);
            }
        }

        // Handle rotation when moving
        if (moveDirection != Vector3.zero)
        {
            animator.SetBool("IsMoving", true);
            // Calculate target rotation based on movement direction
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            // Smoothly rotate towards target direction
            transform.rotation = Quaternion.RotateTowards(transform.rotation,
                toRotation, rotationMaxDegree * Time.deltaTime);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

        // Apply movement when in air (grounded movement handled by root motion)
        if (!isGrounded)
        {
            Vector3 velocity = moveDirection * inputMagnitude * jumpHeight;
            velocity.y = yGravity;
            characterController.Move(velocity * Time.deltaTime);
        }
    }

    /// <summary>
    /// Called after the Animator has updated. Applies root motion from animations.
    /// This allows animations to drive movement while still applying gravity.
    /// </summary>
    private void OnAnimatorMove()
    {
        // Use animation's delta position for movement (root motion)
        Vector3 velocity = animator.deltaPosition;
        // Apply gravity to vertical component
        velocity.y = yGravity * Time.deltaTime;
        characterController.Move(velocity);
    }
}
