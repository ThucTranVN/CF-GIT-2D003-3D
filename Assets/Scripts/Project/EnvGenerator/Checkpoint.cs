using UnityEngine;

/// <summary>
/// Checkpoint trigger that extends game time and increases level speed when the player passes through.
/// Represents a milestone in the endless runner that rewards the player.
/// </summary>
public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    /// <summary>
    /// Amount of time (in seconds) to add to the game timer when checkpoint is reached.
    /// </summary>
    private float checkpointTimeExtension = 5f;

    [SerializeField]
    /// <summary>
    /// Amount to increase the chunk movement speed when checkpoint is reached.
    /// Makes the game progressively faster.
    /// </summary>
    private float adjustChangeMoveSpeedAmount = 3f;

    /// <summary>
    /// Tag string for identifying the player GameObject.
    /// </summary>
    private const string playerTag = "Player";

    /// <summary>
    /// Called when another collider enters the trigger zone.
    /// Checks if it's the player and applies checkpoint rewards.
    /// </summary>
    /// <param name="other">The collider that entered the trigger.</param>
    void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the player
        if (other.gameObject.CompareTag(playerTag))
        {
            Debug.Log("Checkpoint reached");

            // Add time to the game timer
            if (GameManager.HasInstance)
            {
                GameManager.Instance.IncreaseTime(checkpointTimeExtension);
            }
            
            // Increase level speed for increased difficulty
            if (LevelGenerator.HasInstance)
            {
                LevelGenerator.Instance.ChangeChunkMoveSpeed(adjustChangeMoveSpeedAmount);
            }
        }
    }
}
