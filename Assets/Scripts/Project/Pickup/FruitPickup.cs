using UnityEngine;

public class FruitPickup : Pickup
{
    [SerializeField]
    private float adjustChangeMoveSpeedAmount = 3f;

    protected override void OnPickUp()
    {
        Debug.Log("Add 100 energy");
        if (LevelGenerator.HasInstance)
        {
            LevelGenerator.Instance.ChangeChunkMoveSpeed(adjustChangeMoveSpeedAmount);
        }
    }
}
