using UnityEngine;
using System.Linq;

/// <summary>
/// Demo script demonstrating ragdoll physics activation and control.
/// Manages switching between animated and ragdoll physics states, and applies forces to ragdoll parts.
/// </summary>
public class RagdollDemo : MonoBehaviour
{
    /// <summary>
    /// Reference to the main camera (currently unused but stored for potential use).
    /// </summary>
    private Camera mainCamera;
    
    /// <summary>
    /// Array of all Rigidbody components in the ragdoll hierarchy.
    /// </summary>
    private Rigidbody[] ragdollRigidbodies;
    
    /// <summary>
    /// Reference to the Animator component that controls normal animations.
    /// </summary>
    private Animator animator;

    /// <summary>
    /// Initializes component references, finds all ragdoll rigidbodies, and disables ragdoll by default.
    /// </summary>
    void Start()
    {
        mainCamera = Camera.main;
        animator = GetComponent<Animator>();
        // Get all Rigidbody components in children (ragdoll parts)
        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        // Start with ragdoll disabled (normal animation mode)
        DisableRagdoll();
    }

    /// <summary>
    /// Triggers ragdoll physics by enabling ragdoll and applying force at the hit point.
    /// </summary>
    /// <param name="force">The force vector to apply to the ragdoll.</param>
    /// <param name="hitPoint">The world position where the force should be applied.</param>
    public void TriggerRagdoll(Vector3 force, Vector3 hitPoint)
    {
        // Enable ragdoll physics
        EnableRagdoll();
        
        // Find the closest rigidbody to the hit point
        Rigidbody hitRigidbody = FindClosetHitRigidbody(hitPoint);
        
        // Apply force at the hit position if a rigidbody was found
        if (hitRigidbody != null)
        {
            hitRigidbody.AddForceAtPosition(force, hitPoint, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// Finds the ragdoll rigidbody closest to the specified hit point.
    /// </summary>
    /// <param name="hitPoint">The world position to find the nearest rigidbody to.</param>
    /// <returns>The Rigidbody component closest to the hit point, or null if no rigidbodies exist.</returns>
    private Rigidbody FindClosetHitRigidbody(Vector3 hitPoint)
    {
        Rigidbody closetRigidbody = null;
        float closestDistance = 0;

        // Iterate through all ragdoll rigidbodies to find the closest one
        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            float distance = Vector3.Distance(rb.position, hitPoint);
            // Update closest if this is the first rigidbody or closer than previous closest
            if (closetRigidbody == null || distance < closestDistance)
            {
                closestDistance = distance;
                closetRigidbody = rb;
            }
        }

        return closetRigidbody;
    }

    /// <summary>
    /// Disables ragdoll physics by making all rigidbodies kinematic and enabling the animator.
    /// This allows normal animation-driven movement.
    /// </summary>
    private void DisableRagdoll()
    {
        if(ragdollRigidbodies?.Length > 0)
        {
            // Make all ragdoll parts kinematic (not affected by physics)
            foreach (Rigidbody rb in ragdollRigidbodies)
            {
                rb.isKinematic = true;
            }

            // Enable animator for normal animation control
            animator.enabled = true;
        }
    }

    /// <summary>
    /// Enables ragdoll physics by making all rigidbodies non-kinematic and disabling the animator.
    /// This allows physics to take control of the character.
    /// </summary>
    private void EnableRagdoll()
    {
        if (ragdollRigidbodies?.Length > 0)
        {
            // Make all ragdoll parts non-kinematic (affected by physics)
            foreach (Rigidbody rb in ragdollRigidbodies)
            {
                rb.isKinematic = false;
            }

            // Disable animator so physics can control movement
            animator.enabled = false;
        }
    }
}
