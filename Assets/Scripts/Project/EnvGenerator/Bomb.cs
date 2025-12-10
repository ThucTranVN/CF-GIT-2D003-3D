using UnityEngine;
using Unity.Cinemachine;

/// <summary>
/// Bomb object that triggers camera shake and particle effects on collision.
/// Uses Cinemachine Impulse Source for camera shake effects.
/// </summary>
public class Bomb : MonoBehaviour
{
    [SerializeField]
    /// <summary>
    /// Modifier to adjust the intensity of camera shake (multiplier for distance-based calculation).
    /// </summary>
    private float shakeModifier = 0.5f;
    
    /// <summary>
    /// Reference to the Cinemachine Impulse Source component for camera shake.
    /// </summary>
    private CinemachineImpulseSource impulseSource;
    
    [SerializeField]
    /// <summary>
    /// Particle system effect to play at the collision point.
    /// </summary>
    private ParticleSystem collisionEffect;

    /// <summary>
    /// Gets the Cinemachine Impulse Source component reference.
    /// </summary>
    void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    /// <summary>
    /// Called when the bomb collides with another object.
    /// Triggers camera shake and plays collision particle effect.
    /// </summary>
    /// <param name="collision">Collision data containing contact information.</param>
    private void OnCollisionEnter(Collision collision)
    {
        FireImpulse();
        CollisionEffect(collision);
    }

    /// <summary>
    /// Generates a camera shake impulse based on distance from the camera.
    /// Closer explosions create stronger shake effects.
    /// </summary>
    private void FireImpulse()
    {
        // Calculate distance from bomb to camera
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        // Calculate shake intensity: closer = stronger shake (inverse distance relationship)
        float shakeIntensity = (1f / distance) * shakeModifier;
        // Clamp intensity to prevent excessive shake
        shakeIntensity = Mathf.Min(shakeIntensity, 1f);
        // Generate the camera shake impulse
        impulseSource.GenerateImpulse(shakeIntensity);
    }

    /// <summary>
    /// Plays the collision particle effect at the contact point.
    /// </summary>
    /// <param name="collision">Collision data containing the contact point information.</param>
    private void CollisionEffect(Collision collision)
    {
        // Get the first contact point from the collision
        ContactPoint contactPoint = collision.contacts[0];
        // Position the effect at the contact point
        collisionEffect.transform.position = contactPoint.point;
        // Play the particle effect
        collisionEffect.Play();
    }
}
