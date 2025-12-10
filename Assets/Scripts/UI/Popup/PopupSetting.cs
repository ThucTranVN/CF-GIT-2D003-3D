using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Settings popup that allows players to adjust game settings.
/// Can be extended to include volume controls, graphics settings, etc.
/// </summary>
public class PopupSetting : BasePopup
{
    [SerializeField]
    /// <summary>
    /// Button component for closing the settings popup.
    /// </summary>
    private Button btnClose;

    /// <summary>
    /// Initializes the settings popup.
    /// </summary>
    public override void Init()
    {
        base.Init();
    }

    /// <summary>
    /// Shows the settings popup.
    /// </summary>
    /// <param name="data">Optional data to pass to the popup.</param>
    public override void Show(object data)
    {
        base.Show(data);
    }

    /// <summary>
    /// Hides the settings popup.
    /// </summary>
    public override void Hide()
    {
        base.Hide();
    }
}
