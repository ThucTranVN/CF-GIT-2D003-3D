using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Popup displayed when the game ends, showing rewards or final score.
/// Allows player to exit the game after viewing results.
/// </summary>
public class PopupReward : BasePopup
{
    /// <summary>
    /// Initializes the popup component.
    /// </summary>
    public override void Init()
    {
        base.Init();
    }

    /// <summary>
    /// Hides the popup from view.
    /// </summary>
    public override void Hide()
    {
        base.Hide();
    }

    /// <summary>
    /// Shows the popup with optional data.
    /// </summary>
    /// <param name="data">Optional data to pass to the popup (e.g., score, rewards).</param>
    public override void Show(object data)
    {
        base.Show(data);
    }

    /// <summary>
    /// Called when the continue button is clicked.
    /// Hides the popup and exits the game.
    /// </summary>
    public void OnClickBtnContinue()
    {
        this.Hide();

        // Exit the game
        if (GameManager.HasInstance)
        {
            GameManager.Instance.ExitGame();
        }
    }
}
