using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : BaseManager<GameManager>
{
    [SerializeField]
    private float startTime = 5f;

    private float timeLeft;
    private bool isGameOver;

    public bool IsGameOver => isGameOver;

    void Start()
    {
        timeLeft = startTime;

        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowScreen<ScreenGame>();
        }
    }

    private void Update()
    {
        DecreaseTime();
    }

    public void IncreaseTime(float amount)
    {
        timeLeft += amount;
    }

    private void DecreaseTime()
    {
        if (isGameOver) return;

        timeLeft -= Time.deltaTime;

        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.BroadCast(ListenType.ON_TIMER_CHANGE, timeLeft);
        }

        if (timeLeft <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0.1f;
        if (PlayerController.HasInstance)
        {
            PlayerController.Instance.enabled = false;
        }

        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowPopup<PopupReward>();
        }

        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.UnregisterAll(null);
        }
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
