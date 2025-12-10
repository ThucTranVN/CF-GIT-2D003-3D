using UnityEngine;

/// <summary>
/// Base class for all Screen UI elements (full-screen UI like main menu, game screen).
/// Sets the UI type to Screen and provides template methods for derived screens.
/// </summary>
public class BaseScreen : BaseUIElement
{
    /// <summary>
    /// Initializes the screen and sets its UI type.
    /// </summary>
    public override void Init()
    {
        base.Init();
        this.uiType = UIType.Screen;
    }

    /// <summary>
    /// Hides the screen from view.
    /// </summary>
    public override void Hide()
    {
        base.Hide();
    }

    /// <summary>
    /// Shows the screen with optional data.
    /// </summary>
    /// <param name="data">Optional data to pass to the screen.</param>
    public override void Show(object data)
    {
        base.Show(data);
    }

    /// <summary>
    /// Handles back button click events.
    /// Override in derived classes to implement specific back button behavior.
    /// </summary>
    public override void OnClickedBackButton()
    {
        base.OnClickedBackButton();
    }
}
