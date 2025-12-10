using UnityEngine;

/// <summary>
/// Base class for all Overlap UI elements (full-screen overlays like fade effects, loading screens).
/// Sets the UI type to Overlap and provides template methods for derived overlaps.
/// </summary>
public class BaseOverlap : BaseUIElement
{
    /// <summary>
    /// Initializes the overlap and sets its UI type.
    /// </summary>
    public override void Init()
    {
        base.Init();
        this.uiType = UIType.Overlap;
    }

    /// <summary>
    /// Hides the overlap from view.
    /// </summary>
    public override void Hide()
    {
        base.Hide();
    }

    /// <summary>
    /// Shows the overlap with optional data.
    /// </summary>
    /// <param name="data">Optional data to pass to the overlap.</param>
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
