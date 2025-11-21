using UnityEngine;

public class ScoreManager : BaseManager<ScoreManager>
{
    private int currentScore = 0;

    public void IncreaseScore(int amount)
    {
        if (GameManager.HasInstance)
        {
            if (GameManager.Instance.IsGameOver) return;
        }

        currentScore += amount;
        Debug.Log($"currentScore {currentScore}");

        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.BroadCast(ListenType.ON_PLAYER_UPDATE_COIN, currentScore);
        }
    }
}
