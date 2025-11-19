using UnityEngine;

public class CoinPickup : Pickup
{
    protected override void OnPickUp()
    {
        Debug.Log("Add 100 coins");
    }
}
