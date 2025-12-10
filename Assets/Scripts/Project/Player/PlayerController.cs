using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controller for player movement using the new Input System.
/// Handles input, movement, and position clamping within defined boundaries.
/// </summary>
public class PlayerController : BaseManager<PlayerController>
{
    [Header("Movement Settings")]
    [SerializeField]
    /// <summary>
    /// Speed at which the player moves (units per second).
    /// </summary>
    private float moveSpeed = 5f;
    
    [SerializeField]
    /// <summary>
    /// Maximum distance the player can move on the X axis (left/right boundary).
    /// </summary>
    private float xClamp = 2f;
    
    [SerializeField]
    /// <summary>
    /// Maximum distance the player can move on the Y axis (up/down boundary).
    /// </summary>
    private float yClamp = 2f;
    
    /// <summary>
    /// Current movement input vector from the input system.
    /// </summary>
    private Vector2 movement;
    
    /// <summary>
    /// Reference to the Rigidbody component for physics-based movement.
    /// </summary>
    private Rigidbody rigidbody;

    /// <summary>
    /// Initializes the Rigidbody component reference.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();

        rigidbody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Handles movement in FixedUpdate for consistent physics-based movement.
    /// </summary>
    private void FixedUpdate()
    {
        HandleMoveMent();
    }

    /// <summary>
    /// Callback method for the Input System's Move action.
    /// Called when movement input is received.
    /// </summary>
    /// <param name="context">Input action context containing the input value.</param>
    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// Processes movement input and applies it to the player's position.
    /// Clamps the position within defined boundaries to keep player in bounds.
    /// </summary>
    private void HandleMoveMent()
    {
        Vector3 currentPosition = rigidbody.position;
        // Convert 2D input to 3D movement (X and Y input map to X and Z in world space)
        Vector3 moveDirection = new Vector3(movement.x, 0f, movement.y);
        // Calculate new position based on movement direction and speed
        Vector3 newPosition = currentPosition + moveDirection * (moveSpeed * Time.fixedDeltaTime);
        // Clamp position to keep player within boundaries
        newPosition.x = Mathf.Clamp(newPosition.x, -xClamp, xClamp);
        newPosition.y = Mathf.Clamp(newPosition.y, -yClamp, yClamp);
        // Apply the new position using physics-based movement
        rigidbody.MovePosition(newPosition);
    }
}
