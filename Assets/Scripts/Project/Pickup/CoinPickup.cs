using UnityEngine;

/// <summary>
/// Coin pickup that increases the player's score when collected.
/// Inherits from Pickup base class and implements score increase behavior.
/// </summary>
public class CoinPickup : Pickup
{
    [SerializeField]
    /// <summary>
    /// Amount of score points to award when this coin is collected.
    /// </summary>
    private int cointAmount = 100;

    /// <summary>
    /// Called when the coin is picked up by the player.
    /// Increases the score and spawns a pickup effect.
    /// </summary>
    protected override void OnPickUp()
    {
        Debug.Log("Add 100 coins");

        // Increase player score
        if (ScoreManager.HasInstance)
        {
            ScoreManager.Instance.IncreaseScore(cointAmount);
        }

        // Spawn visual effect if one is assigned
        if (pickupEffect != null)
        {
            Instantiate(pickupEffect, spawnEffectPosition.position, Quaternion.identity);
        }
    }
}
