using UnityEngine;

/// <summary>
/// Base class for all UI elements in the game (Screens, Popups, Notifies, Overlaps).
/// Provides common functionality for initialization, showing, hiding, and canvas group management.
/// </summary>
public class BaseUIElement : MonoBehaviour
{
    /// <summary>
    /// CanvasGroup component used for controlling visibility and raycast blocking.
    /// </summary>
    protected CanvasGroup canvasGroup;
    
    /// <summary>
    /// Type of UI element (Screen, Popup, Notify, or Overlap).
    /// </summary>
    protected UIType uiType = UIType.Unknown;
    
    /// <summary>
    /// Flag indicating whether this UI element is currently hidden.
    /// </summary>
    protected bool isHide;
    
    /// <summary>
    /// Flag indicating whether this UI element has been initialized.
    /// </summary>
    private bool isInited;

    /// <summary>
    /// Public property to check if the UI element is hidden.
    /// </summary>
    public bool IsHide { get => isHide; }
    
    /// <summary>
    /// Public property to access the CanvasGroup component.
    /// </summary>
    public CanvasGroup CanvasGroup { get => canvasGroup; }
    
    /// <summary>
    /// Public property to check if the UI element has been initialized.
    /// </summary>
    public bool IsInited { get => isInited; }
    
    /// <summary>
    /// Public property to get the UI element type.
    /// </summary>
    public UIType UIType { get => uiType; }

    /// <summary>
    /// Initializes the UI element: creates CanvasGroup if needed, activates GameObject, and hides it.
    /// Must be called before using the UI element.
    /// </summary>
    public virtual void Init()
    {
        this.isInited = true;
        // Add CanvasGroup component if it doesn't exist
        if (!this.gameObject.GetComponent<CanvasGroup>())
        {
            this.gameObject.AddComponent<CanvasGroup>();
        }
        this.canvasGroup = this.gameObject.GetComponent<CanvasGroup>();
        this.gameObject.SetActive(true);

        // Start hidden
        Hide();
    }

    /// <summary>
    /// Shows the UI element by activating it and making it visible.
    /// </summary>
    /// <param name="data">Optional data to pass to the UI element (e.g., score, settings).</param>
    public virtual void Show(object data)
    {
        this.gameObject.SetActive(true);
        this.isHide = false;
        SetActiveGroupCanvas(true);
    }

    /// <summary>
    /// Hides the UI element by making it invisible and non-interactive.
    /// </summary>
    public virtual void Hide()
    {
        this.isHide = true;
        SetActiveGroupCanvas(false);
    }

    /// <summary>
    /// Virtual method for clearing/resetting UI element state.
    /// Override in derived classes to implement cleanup logic.
    /// </summary>
    public virtual void Clear()
    {

    }

    /// <summary>
    /// Sets the CanvasGroup's alpha and raycast blocking based on active state.
    /// </summary>
    /// <param name="isAct">True to make visible and interactive, false to hide and disable interaction.</param>
    private void SetActiveGroupCanvas(bool isAct)
    {
        if (CanvasGroup != null)
        {
            CanvasGroup.blocksRaycasts = isAct;
            CanvasGroup.alpha = isAct ? 1 : 0;
        }
    }

    /// <summary>
    /// Virtual method for handling back button clicks.
    /// Override in derived classes to implement back button behavior.
    /// </summary>
    public virtual void OnClickedBackButton()
    {

    }
}
