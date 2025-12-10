using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Main game manager that handles core game flow, timer, and game over logic.
/// Manages the game state and coordinates between different systems.
/// </summary>
public class GameManager : BaseManager<GameManager>
{
    [SerializeField]
    /// <summary>
    /// The initial time value when the game starts (in seconds).
    /// </summary>
    private float startTime = 5f;

    /// <summary>
    /// The current remaining time in the game timer.
    /// </summary>
    private float timeLeft;
    
    /// <summary>
    /// Flag indicating whether the game has ended.
    /// </summary>
    private bool isGameOver;

    /// <summary>
    /// Public property to check if the game is over.
    /// </summary>
    public bool IsGameOver => isGameOver;

    /// <summary>
    /// Initializes the game timer and shows the game screen.
    /// </summary>
    void Start()
    {
        timeLeft = startTime;

        // Show the game screen UI
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowScreen<ScreenGame>();
        }
    }

    /// <summary>
    /// Updates the game timer every frame.
    /// </summary>
    private void Update()
    {
        DecreaseTime();
    }

    /// <summary>
    /// Adds time to the game timer. Can be called when player collects items or completes objectives.
    /// </summary>
    /// <param name="amount">The amount of time to add (in seconds).</param>
    public void IncreaseTime(float amount)
    {
        timeLeft += amount;
    }

    /// <summary>
    /// Decreases the timer by deltaTime each frame.
    /// Broadcasts timer updates and triggers game over when time reaches zero.
    /// </summary>
    private void DecreaseTime()
    {
        // Don't decrease time if game is already over
        if (isGameOver) return;

        // Decrease time by frame delta
        timeLeft -= Time.deltaTime;

        // Broadcast timer change event to all listeners (e.g., UI updates)
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.BroadCast(ListenType.ON_TIMER_CHANGE, timeLeft);
        }

        // Check if time has run out
        if (timeLeft <= 0)
        {
            GameOver();
        }
    }

    /// <summary>
    /// Handles game over logic: slows down time, disables player, shows reward popup, and cleans up listeners.
    /// </summary>
    private void GameOver()
    {
        isGameOver = true;
        
        // Slow down time for dramatic effect
        Time.timeScale = 0.1f;
        
        // Disable player controller
        if (PlayerController.HasInstance)
        {
            PlayerController.Instance.enabled = false;
        }

        // Show reward popup
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowPopup<PopupReward>();
        }

        // Clean up all event listeners
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.UnregisterAll(null);
        }
    }

    /// <summary>
    /// Exits the game. Works in both editor and build.
    /// </summary>
    public void ExitGame()
    {
#if UNITY_EDITOR
        // Stop play mode in editor
        EditorApplication.isPlaying = false;
#else
        // Quit application in build
        Application.Quit();
#endif
    }
}
