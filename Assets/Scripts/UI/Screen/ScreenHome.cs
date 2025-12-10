using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Home/main menu screen displayed at game start.
/// Provides buttons to start the game and access settings.
/// </summary>
public class ScreenHome : BaseScreen
{
    [SerializeField]
    /// <summary>
    /// Button component for opening the settings popup.
    /// </summary>
    private Button btnSetting;

    /// <summary>
    /// Initializes the home screen.
    /// </summary>
    public override void Init()
    {
        base.Init();
    }

    /// <summary>
    /// Shows the home screen.
    /// </summary>
    /// <param name="data">Optional data to pass to the screen.</param>
    public override void Show(object data)
    {
        base.Show(data);
    }

    /// <summary>
    /// Hides the home screen.
    /// </summary>
    public override void Hide()
    {
        base.Hide();
    }

    /// <summary>
    /// Called when the settings button is clicked.
    /// Opens the settings popup.
    /// </summary>
    public void OnClickSettingButton()
    {
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowPopup<PopupSetting>();
        }
    }

    /// <summary>
    /// Called when the start game button is clicked.
    /// Shows the loading notify and hides the home screen to begin gameplay.
    /// </summary>
    public void OnClickStartGame()
    {
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowNotify<NotifyLoadingGame>();
        }

        this.Hide();
    }
}
