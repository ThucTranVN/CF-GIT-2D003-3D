using UnityEngine;

/// <summary>
/// Demo script demonstrating chargeable shooting mechanics with ragdoll interaction.
/// Allows players to charge a shot by holding the mouse button, then fires with proportional force.
/// </summary>
public class ShootDemo : MonoBehaviour
{
    [SerializeField]
    /// <summary>
    /// The maximum force that can be applied when fully charged.
    /// </summary>
    private float maximumForce;
    
    [SerializeField]
    /// <summary>
    /// The time (in seconds) required to reach maximum charge.
    /// </summary>
    private float maximumForceTime;

    /// <summary>
    /// Timestamp when the mouse button was pressed down (for charge calculation).
    /// </summary>
    private float timeHoldMouseButtonDown;
    
    /// <summary>
    /// Reference to the Camera component for raycasting.
    /// </summary>
    private Camera camera;

    /// <summary>
    /// Gets the Camera component reference.
    /// </summary>
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    /// <summary>
    /// Handles mouse input for charging and shooting at ragdoll targets.
    /// </summary>
    void Update()
    {
        // Record when mouse button is pressed (start charging)
        if (Input.GetMouseButtonDown(0))
        {
            timeHoldMouseButtonDown = Time.time;
        }

        // When mouse button is released, fire the shot
        if (Input.GetMouseButtonUp(0))
        {
            // Create a ray from camera through mouse position
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            // Cast ray to detect what was clicked
            if(Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                // Try to find RagdollDemo component in the hit object's hierarchy
                RagdollDemo ragdollDemo = hitInfo.collider.GetComponentInParent<RagdollDemo>();

                if(ragdollDemo != null)
                {
                    // Calculate charge duration
                    float mouseButtonDownDuration = Time.time - timeHoldMouseButtonDown;
                    // Calculate force percentage based on charge time (0 to 1)
                    float forcePercentage = mouseButtonDownDuration / maximumForceTime;
                    // Lerp force from minimum (1) to maximum based on charge percentage
                    float forceMagnitude = Mathf.Lerp(1, maximumForce, forcePercentage);

                    // Calculate force direction from camera to ragdoll
                    Vector3 forceDirection = ragdollDemo.transform.position - camera.transform.position;
                    forceDirection.y = 1; // Add upward component
                    forceDirection.Normalize();

                    // Calculate final force vector
                    Vector3 force = forceMagnitude * forceDirection;

                    // Trigger ragdoll with calculated force at hit point
                    ragdollDemo.TriggerRagdoll(force, hitInfo.point);
                }
            }
        }
    }
}
