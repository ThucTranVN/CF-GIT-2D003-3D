using UnityEngine;

/// <summary>
/// Base class for all Notify UI elements (temporary messages, alerts, notifications).
/// Sets the UI type to Notify and provides template methods for derived notifies.
/// </summary>
public class BaseNotify : BaseUIElement
{
    /// <summary>
    /// Initializes the notify and sets its UI type.
    /// </summary>
    public override void Init()
    {
        base.Init();
        this.uiType = UIType.Notify;
    }

    /// <summary>
    /// Hides the notify from view.
    /// </summary>
    public override void Hide()
    {
        base.Hide();
    }

    /// <summary>
    /// Shows the notify with optional data.
    /// </summary>
    /// <param name="data">Optional data to pass to the notify.</param>
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
