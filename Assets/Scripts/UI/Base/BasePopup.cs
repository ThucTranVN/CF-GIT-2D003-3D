using UnityEngine;

/// <summary>
/// Base class for all Popup UI elements (dialog boxes, settings, rewards).
/// Sets the UI type to Popup and provides template methods for derived popups.
/// </summary>
public class BasePopup : BaseUIElement
{
    /// <summary>
    /// Initializes the popup and sets its UI type.
    /// </summary>
    public override void Init()
    {
        base.Init();
        this.uiType = UIType.Popup;
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
    /// <param name="data">Optional data to pass to the popup.</param>
    public override void Show(object data)
    {
        base.Show(data);
    }

    /// <summary>
    /// Handles back button click events.
    /// Override in derived classes to implement specific back button behavior (e.g., close popup).
    /// </summary>
    public override void OnClickedBackButton()
    {
        base.OnClickedBackButton();
    }
}
