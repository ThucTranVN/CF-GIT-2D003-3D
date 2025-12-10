using UnityEngine;
using TMPro;

/// <summary>
/// Main game screen that displays gameplay UI elements.
/// Shows coin count and timer, and listens to game events for updates.
/// </summary>
public class ScreenGame : BaseScreen
{
    [Header("UI References")]
    [SerializeField]
    /// <summary>
    /// TextMeshPro text component displaying the current coin/score count.
    /// </summary>
    private TMP_Text txtCoin;
    
    [SerializeField]
    /// <summary>
    /// TextMeshPro text component displaying the remaining game time.
    /// </summary>
    private TMP_Text txtTime;

    /// <summary>
    /// Initializes the screen and registers event listeners for coin and timer updates.
    /// </summary>
    public override void Init()
    {
        base.Init();

        // Register listeners for game events
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Register(ListenType.ON_TIMER_CHANGE, OnTimerUpdate);
            ListenerManager.Instance.Register(ListenType.ON_PLAYER_UPDATE_COIN, OnPlayerUpdateCoin);
        }
    }

    /// <summary>
    /// Shows the game screen.
    /// </summary>
    /// <param name="data">Optional data to pass to the screen.</param>
    public override void Show(object data)
    {
        base.Show(data);
    }

    /// <summary>
    /// Hides the game screen and unregisters event listeners to prevent memory leaks.
    /// </summary>
    public override void Hide()
    {
        base.Hide();

        // Unregister listeners when screen is hidden
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.UnRegister(ListenType.ON_TIMER_CHANGE, OnTimerUpdate);
            ListenerManager.Instance.UnRegister(ListenType.ON_PLAYER_UPDATE_COIN, OnPlayerUpdateCoin);
        }
    }

    /// <summary>
    /// Callback method invoked when coin/score update event is broadcast.
    /// Updates the coin display text with the new value.
    /// </summary>
    /// <param name="value">The coin/score value (expected to be an int).</param>
    private void OnPlayerUpdateCoin(object value)
    {
        if(value != null && value is int coin)
        {
            txtCoin.text = coin.ToString();
        }
    }

    /// <summary>
    /// Callback method invoked when timer update event is broadcast.
    /// Updates the timer display text with the remaining time.
    /// </summary>
    /// <param name="value">The time value (expected to be a float).</param>
    private void OnTimerUpdate(object value)
    {
        if(value != null && value is float time)
        {
            // Display time with one decimal place
            txtTime.text = time.ToString("F1");
        }
    }
}
