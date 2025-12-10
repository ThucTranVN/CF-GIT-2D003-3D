using UnityEngine;

/// <summary>
/// Handles player collision with obstacles and triggers appropriate responses.
/// Slows down level speed on collision and plays hit animation with cooldown protection.
/// </summary>
public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField]
    /// <summary>
    /// Reference to the Animator component for playing hit animations.
    /// </summary>
    private Animator animator;
    
    [SerializeField]
    /// <summary>
    /// Cooldown time (in seconds) between collision responses to prevent spam.
    /// </summary>
    private float collisionCooldown = 1f;
    
    [SerializeField]
    /// <summary>
    /// Amount to decrease chunk movement speed when player collides with obstacles.
    /// Negative value slows down the level.
    /// </summary>
    private float adjustChunkMoveSpeed = -2f;
    
    /// <summary>
    /// Timer tracking time since last collision (for cooldown).
    /// </summary>
    private float coolDownTimer = 0f;

    /// <summary>
    /// Animator trigger parameter name for hit animation.
    /// </summary>
    private const string hitString = "Hit";

    /// <summary>
    /// Called when the player collides with another object.
    /// Slows down level speed and plays hit animation if cooldown has passed.
    /// </summary>
    /// <param name="collision">Collision data containing information about the collision.</param>
    private void OnCollisionEnter(Collision collision)
    {
        // Check if cooldown period has passed
        if (coolDownTimer < collisionCooldown) return;

        // Slow down level speed as penalty for collision
        if (LevelGenerator.HasInstance)
        {
            LevelGenerator.Instance.ChangeChunkMoveSpeed(adjustChunkMoveSpeed);
        }
        
        // Play hit animation
        animator.SetTrigger(hitString);
        // Reset cooldown timer
        coolDownTimer = 0f;
    }

    /// <summary>
    /// Updates the cooldown timer every frame.
    /// </summary>
    private void Update()
    {
        coolDownTimer += Time.deltaTime;
    }
}
