using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    private float checkpointTimeExtension = 5f;

    [SerializeField]
    private float adjustChangeMoveSpeedAmount = 3f;

    private const string playerTag = "Player";

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            Debug.Log("Checkpoint reached");

            if (GameManager.HasInstance)
            {
                GameManager.Instance.IncreaseTime(checkpointTimeExtension);
            }
            
            if (LevelGenerator.HasInstance)
            {
                LevelGenerator.Instance.ChangeChunkMoveSpeed(adjustChangeMoveSpeedAmount);
            }
        }
    }
}
