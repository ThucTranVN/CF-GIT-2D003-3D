using UnityEngine;

public class FruitPickup : Pickup
{
    protected override void OnPickUp()
    {
        if (pickupEffect != null)
        {
            Instantiate(pickupEffect, spawnEffectPosition.position, Quaternion.identity);
        }
    }
}
