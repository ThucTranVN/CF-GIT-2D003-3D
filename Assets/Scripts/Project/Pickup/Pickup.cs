using UnityEngine;

/// <summary>
/// Abstract base class for all pickup items in the game.
/// Handles trigger detection and provides a template method pattern for pickup behavior.
/// Derived classes must implement OnPickUp() to define specific pickup effects.
/// </summary>
public abstract class Pickup : MonoBehaviour
{
    [SerializeField]
    /// <summary>
    /// Particle effect or visual effect GameObject to spawn when picked up.
    /// </summary>
    protected GameObject pickupEffect;
    
    [SerializeField]
    /// <summary>
    /// Transform position where the pickup effect should be spawned.
    /// </summary>
    protected Transform spawnEffectPosition;
    
    /// <summary>
    /// Tag string for identifying the player GameObject.
    /// </summary>
    private const string playerTag = "Player";

    /// <summary>
    /// Called when another collider enters the trigger zone.
    /// Checks if it's the player and triggers pickup behavior.
    /// </summary>
    /// <param name="other">The collider that entered the trigger.</param>
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the player
        if (other.gameObject.CompareTag(playerTag))
        {
            // Call the abstract method for specific pickup behavior
            OnPickUp();
            // Destroy the pickup object after collection
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Abstract method that must be implemented by derived classes.
    /// Defines the specific behavior when this pickup is collected.
    /// </summary>
    protected abstract void OnPickUp();
}
