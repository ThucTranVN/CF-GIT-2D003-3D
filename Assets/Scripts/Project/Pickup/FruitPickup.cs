using UnityEngine;

/// <summary>
/// Fruit pickup that extends game time when collected.
/// Inherits from Pickup base class. Currently only spawns visual effects.
/// </summary>
public class FruitPickup : Pickup
{
    /// <summary>
    /// Called when the fruit is picked up by the player.
    /// Spawns a pickup effect. Time extension logic can be added here.
    /// </summary>
    protected override void OnPickUp()
    {
        // Spawn visual effect if one is assigned
        if (pickupEffect != null)
        {
            Instantiate(pickupEffect, spawnEffectPosition.position, Quaternion.identity);
        }
        
        // TODO: Add time extension logic here
        // Example: GameManager.Instance.IncreaseTime(timeAmount);
    }
}
