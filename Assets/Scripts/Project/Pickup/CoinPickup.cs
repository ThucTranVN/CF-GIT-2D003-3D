using UnityEngine;

public class CoinPickup : Pickup
{
    [SerializeField]
    private int cointAmount = 100;

    protected override void OnPickUp()
    {
        Debug.Log("Add 100 coins");

        if (ScoreManager.HasInstance)
        {
            ScoreManager.Instance.IncreaseScore(cointAmount);
        }

        if (pickupEffect != null)
        {
            Instantiate(pickupEffect, spawnEffectPosition.position, Quaternion.identity);
        }
    }
}
