using UnityEngine;

/// <summary>
/// Manager responsible for tracking and updating the player's score.
/// Broadcasts score updates through the event system for UI and other systems to respond.
/// </summary>
public class ScoreManager : BaseManager<ScoreManager>
{
    /// <summary>
    /// Current score value accumulated by the player.
    /// </summary>
    private int currentScore = 0;

    /// <summary>
    /// Increases the player's score by the specified amount.
    /// Only works if the game is not over, and broadcasts the update to listeners.
    /// </summary>
    /// <param name="amount">The amount to add to the current score.</param>
    public void IncreaseScore(int amount)
    {
        // Don't increase score if game is over
        if (GameManager.HasInstance)
        {
            if (GameManager.Instance.IsGameOver) return;
        }

        // Update score
        currentScore += amount;
        Debug.Log($"currentScore {currentScore}");

        // Broadcast score update event for UI and other systems
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.BroadCast(ListenType.ON_PLAYER_UPDATE_COIN, currentScore);
        }
    }
}
