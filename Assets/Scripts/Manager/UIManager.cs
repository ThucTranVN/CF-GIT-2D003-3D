using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manager responsible for creating, showing, hiding, and managing all UI elements.
/// Handles Screens, Popups, Notifies, and Overlaps with automatic instantiation and pooling.
/// </summary>
public class UIManager : BaseManager<UIManager>
{
    [Header("UI Container References")]
    /// <summary>
    /// Parent GameObject container for all Screen UI elements.
    /// </summary>
    public GameObject cScreen, cPopup, cNotify, cOverlap;
    
    /// <summary>
    /// The camera used for rendering UI elements.
    /// </summary>
    public Camera UICamera;
    
    /// <summary>
    /// Dictionary storing all instantiated screens by their class name.
    /// </summary>
    private Dictionary<string, BaseScreen> screens = new Dictionary<string, BaseScreen>();
    
    /// <summary>
    /// Dictionary storing all instantiated popups by their class name.
    /// </summary>
    private Dictionary<string, BasePopup> popups = new Dictionary<string, BasePopup>();
    
    /// <summary>
    /// Dictionary storing all instantiated notifies by their class name.
    /// </summary>
    private Dictionary<string, BaseNotify> notifies = new Dictionary<string, BaseNotify>();
    
    /// <summary>
    /// Dictionary storing all instantiated overlaps by their class name.
    /// </summary>
    private Dictionary<string, BaseOverlap> overlaps = new Dictionary<string, BaseOverlap>();

    /// <summary>
    /// Public accessor for all registered screens.
    /// </summary>
    public Dictionary<string, BaseScreen> Screens => screens;
    
    /// <summary>
    /// Public accessor for all registered popups.
    /// </summary>
    public Dictionary<string, BasePopup> Popups => popups;
    
    /// <summary>
    /// Public accessor for all registered notifies.
    /// </summary>
    public Dictionary<string, BaseNotify> Notifies => notifies;
    
    /// <summary>
    /// Public accessor for all registered overlaps.
    /// </summary>
    public Dictionary<string, BaseOverlap> Overlaps => overlaps;

    /// <summary>
    /// Reference to the currently active/visible screen.
    /// </summary>
    private BaseScreen curScreen;
    
    /// <summary>
    /// Reference to the currently active/visible popup.
    /// </summary>
    private BasePopup curPopup;
    
    /// <summary>
    /// Reference to the currently active/visible notify.
    /// </summary>
    private BaseNotify curNotify;
    
    /// <summary>
    /// Reference to the currently active/visible overlap.
    /// </summary>
    private BaseOverlap curOverlap;

    /// <summary>
    /// Public accessor for the current screen.
    /// </summary>
    public BaseScreen CurScreen => curScreen;
    
    /// <summary>
    /// Public accessor for the current popup.
    /// </summary>
    public BasePopup CurPopup => curPopup;
    
    /// <summary>
    /// Public accessor for the current notify.
    /// </summary>
    public BaseNotify CurNotify => curNotify;
    
    /// <summary>
    /// Public accessor for the current overlap.
    /// </summary>
    public BaseOverlap CurOverlap => curOverlap;

    /// <summary>
    /// Resource path for loading screen prefabs from Resources folder.
    /// </summary>
    private const string SCREEN_RESOURCES_PATH = "Prefabs/UI/Screen/";
    
    /// <summary>
    /// Resource path for loading popup prefabs from Resources folder.
    /// </summary>
    private const string POPUP_RESOURCES_PATH = "Prefabs/UI/Popup/";
    
    /// <summary>
    /// Resource path for loading notify prefabs from Resources folder.
    /// </summary>
    private const string NOTIFY_RESOURCES_PATH = "Prefabs/UI/Notify/";
    
    /// <summary>
    /// Resource path for loading overlap prefabs from Resources folder.
    /// </summary>
    private const string OVERLAP_RESOURCES_PATH = "Prefabs/UI/Overlap/";

    #region Screen

    /// <summary>
    /// Instantiates a new screen prefab from Resources and initializes it.
    /// </summary>
    /// <typeparam name="T">The type of screen to create (must inherit from BaseScreen).</typeparam>
    /// <returns>The initialized BaseScreen component.</returns>
    private BaseScreen GetNewScreen<T>() where T : BaseScreen
    {
        string nameScreen = typeof(T).Name;
        GameObject pfScreen = GetUIPrefab(UIType.Screen, nameScreen);
        
        // Validate that the prefab exists and has the required component
        if (pfScreen == null || !pfScreen.GetComponent<BaseScreen>())
        {
            throw new MissingReferenceException("Can not found" + nameScreen + "screen. !!!");
        }
        
        // Instantiate and setup the screen GameObject
        GameObject ob = Instantiate(pfScreen) as GameObject;
        ob.transform.SetParent(this.cScreen.transform);
        ob.transform.localScale = Vector3.one;
        ob.transform.localPosition = Vector3.zero;
#if UNITY_EDITOR
        ob.name = "SCREEN_" + nameScreen;
#endif
        BaseScreen screenScr = ob.GetComponent<BaseScreen>();
        screenScr.Init();
        return screenScr;
    }

    /// <summary>
    /// Hides all currently visible screens.
    /// </summary>
    public void HideAllScreens()
    {
        BaseScreen screenScr = null;

        foreach (KeyValuePair<string, BaseScreen> item in screens)
        {
            screenScr = item.Value;
            // Skip if screen is null or already hidden
            if (screenScr == null || screenScr.IsHide)
                continue;
            screenScr.Hide();

            if (screens.Count <= 0)
                break;
        }
    }

    /// <summary>
    /// Gets an existing screen instance if it has been created before.
    /// </summary>
    /// <typeparam name="T">The type of screen to retrieve.</typeparam>
    /// <returns>The existing screen instance, or null if not found.</returns>
    public T GetExistScreen<T>() where T : BaseScreen
    {
        string screenName = typeof(T).Name;
        if (screens.ContainsKey(screenName))
        {
            return screens[screenName] as T;
        }
        return null;
    }

    /// <summary>
    /// Shows a screen, creating it if it doesn't exist. Reuses existing instance if available.
    /// </summary>
    /// <typeparam name="T">The type of screen to show.</typeparam>
    /// <param name="data">Optional data to pass to the screen's Show method.</param>
    /// <param name="forceShowData">If true, forces the screen to show even if already visible.</param>
    public void ShowScreen<T>(object data = null, bool forceShowData = false) where T : BaseScreen
    {
        string screenName = typeof(T).Name;
        BaseScreen result = null;

        if (curScreen != null)
        {
            var curName = curScreen.GetType().Name;
            if (curName.Equals(screenName))
            {
                result = curScreen;
            }
        }

        if (result == null)
        {
            if (!screens.ContainsKey(screenName))
            {
                BaseScreen screenScr = GetNewScreen<T>();
                if (screenScr != null)
                {
                    screens.Add(screenName, screenScr);
                }
            }

            if (screens.ContainsKey(screenName))
            {
                result = screens[screenName];
            }
        }

        bool isShow = false;
        if (result != null)
        {
            if (forceShowData)
            {
                isShow = true;
            }
            else
            {
                if (result.IsHide)
                {
                    isShow = true;
                }
            }
        }

        if (isShow)
        {
            curScreen = result;
            result.transform.SetAsLastSibling();
            result.Show(data);
        }
    }

    #endregion

    #region Popup

    /// <summary>
    /// Instantiates a new popup prefab from Resources and initializes it.
    /// </summary>
    /// <typeparam name="T">The type of popup to create (must inherit from BasePopup).</typeparam>
    /// <returns>The initialized BasePopup component.</returns>
    private BasePopup GetNewPopup<T>() where T : BasePopup
    {
        string namePopup = typeof(T).Name;
        GameObject pfPopup = GetUIPrefab(UIType.Popup, namePopup);
        
        // Validate that the prefab exists and has the required component
        if (pfPopup == null || !pfPopup.GetComponent<BasePopup>())
        {
            throw new MissingReferenceException("Can not found" + namePopup + "popup. !!!");
        }
        
        // Instantiate and setup the popup GameObject
        GameObject ob = Instantiate(pfPopup) as GameObject;
        ob.transform.SetParent(this.cPopup.transform);
        ob.transform.localScale = Vector3.one;
        ob.transform.localPosition = Vector3.zero;
#if UNITY_EDITOR
        ob.name = "POPUP_" + namePopup;
#endif
        BasePopup popupScr = ob.GetComponent<BasePopup>();
        popupScr.Init();
        return popupScr;
    }

    /// <summary>
    /// Hides all currently visible popups.
    /// </summary>
    public void HideAllPopups()
    {
        BasePopup popupScr = null;

        foreach (KeyValuePair<string, BasePopup> item in popups)
        {
            popupScr = item.Value;
            // Skip if popup is null or already hidden
            if (popupScr == null || popupScr.IsHide)
                continue;
            popupScr.Hide();

            if (popups.Count <= 0)
                break;
        }
    }

    /// <summary>
    /// Gets an existing popup instance if it has been created before.
    /// </summary>
    /// <typeparam name="T">The type of popup to retrieve.</typeparam>
    /// <returns>The existing popup instance, or null if not found.</returns>
    public T GetExistPopup<T>() where T : BasePopup
    {
        string popupName = typeof(T).Name;
        if (popups.ContainsKey(popupName))
        {
            return popups[popupName] as T;
        }
        return null;
    }

    /// <summary>
    /// Shows a popup, creating it if it doesn't exist. Reuses existing instance if available.
    /// </summary>
    /// <typeparam name="T">The type of popup to show.</typeparam>
    /// <param name="data">Optional data to pass to the popup's Show method.</param>
    /// <param name="forceShowData">If true, forces the popup to show even if already visible.</param>
    public void ShowPopup<T>(object data = null, bool forceShowData = false) where T : BasePopup
    {
        string popupName = typeof(T).Name;
        BasePopup result = null;

        if (curPopup != null)
        {
            var curName = curPopup.GetType().Name;
            if (curName.Equals(popupName))
            {
                result = curPopup;
            }
        }

        if (result == null)
        {
            if (!popups.ContainsKey(popupName))
            {
                BasePopup popupScr = GetNewPopup<T>();
                if (popupScr != null)
                {
                    popups.Add(popupName, popupScr);
                }
            }

            if (popups.ContainsKey(popupName))
            {
                result = popups[popupName];
            }
        }

        bool isShow = false;
        if (result != null)
        {
            if (forceShowData)
            {
                isShow = true;
            }
            else
            {
                if (result.IsHide)
                {
                    isShow = true;
                }
            }
        }

        if (isShow)
        {
            curPopup = result;
            result.transform.SetAsLastSibling();
            result.Show(data);
        }
    }

    #endregion

    #region Notify

    /// <summary>
    /// Instantiates a new notify prefab from Resources and initializes it.
    /// </summary>
    /// <typeparam name="T">The type of notify to create (must inherit from BaseNotify).</typeparam>
    /// <returns>The initialized BaseNotify component.</returns>
    private BaseNotify GetNewNotify<T>() where T : BaseNotify
    {
        string nameNotify = typeof(T).Name;
        GameObject pfNotify = GetUIPrefab(UIType.Notify, nameNotify);
        
        // Validate that the prefab exists and has the required component
        if (pfNotify == null || !pfNotify.GetComponent<BaseNotify>())
        {
            throw new MissingReferenceException("Can not found" + nameNotify + "notify. !!!");
        }
        
        // Instantiate and setup the notify GameObject
        GameObject ob = Instantiate(pfNotify) as GameObject;
        ob.transform.SetParent(this.cNotify.transform);
        ob.transform.localScale = Vector3.one;
        ob.transform.localPosition = Vector3.zero;
#if UNITY_EDITOR
        ob.name = "NOTIFY_" + nameNotify;
#endif
        BaseNotify notifyScr = ob.GetComponent<BaseNotify>();
        notifyScr.Init();
        return notifyScr;
    }

    /// <summary>
    /// Hides all currently visible notifies.
    /// </summary>
    public void HideAllNotifies()
    {
        BaseNotify notifyScr = null;

        foreach (KeyValuePair<string, BaseNotify> item in notifies)
        {
            notifyScr = item.Value;
            // Skip if notify is null or already hidden
            if (notifyScr == null || notifyScr.IsHide)
                continue;
            notifyScr.Hide();

            if (notifies.Count <= 0)
                break;
        }
    }

    /// <summary>
    /// Gets an existing notify instance if it has been created before.
    /// </summary>
    /// <typeparam name="T">The type of notify to retrieve.</typeparam>
    /// <returns>The existing notify instance, or null if not found.</returns>
    public T GetExistNotify<T>() where T : BaseNotify
    {
        string notifyName = typeof(T).Name;
        if (notifies.ContainsKey(notifyName))
        {
            return notifies[notifyName] as T;
        }
        return null;
    }

    /// <summary>
    /// Shows a notify, creating it if it doesn't exist. Reuses existing instance if available.
    /// </summary>
    /// <typeparam name="T">The type of notify to show.</typeparam>
    /// <param name="data">Optional data to pass to the notify's Show method.</param>
    /// <param name="forceShowData">If true, forces the notify to show even if already visible.</param>
    public void ShowNotify<T>(object data = null, bool forceShowData = false) where T : BaseNotify
    {
        string notifyName = typeof(T).Name;
        BaseNotify result = null;

        if (curNotify != null)
        {
            var curName = curPopup.GetType().Name;
            if (curName.Equals(notifyName))
            {
                result = curNotify;
            }
        }

        if (result == null)
        {
            if (!notifies.ContainsKey(notifyName))
            {
                BaseNotify notifyScr = GetNewNotify<T>();
                if (notifyScr != null)
                {
                    notifies.Add(notifyName, notifyScr);
                }
            }

            if (notifies.ContainsKey(notifyName))
            {
                result = notifies[notifyName];
            }
        }

        bool isShow = false;
        if (result != null)
        {
            if (forceShowData)
            {
                isShow = true;
            }
            else
            {
                if (result.IsHide)
                {
                    isShow = true;
                }
            }
        }

        if (isShow)
        {
            curNotify = result;
            result.transform.SetAsLastSibling();
            result.Show(data);
        }
    }

    #endregion

    #region Overlap

    /// <summary>
    /// Instantiates a new overlap prefab from Resources and initializes it.
    /// </summary>
    /// <typeparam name="T">The type of overlap to create (must inherit from BaseOverlap).</typeparam>
    /// <returns>The initialized BaseOverlap component.</returns>
    private BaseOverlap GetNewOverLap<T>() where T : BaseOverlap
    {
        string nameOverlap = typeof(T).Name;
        GameObject pfOverlap = GetUIPrefab(UIType.Overlap, nameOverlap);
        
        // Validate that the prefab exists and has the required component
        if (pfOverlap == null || !pfOverlap.GetComponent<BaseOverlap>())
        {
            throw new MissingReferenceException("Can not found" + nameOverlap + "overlap. !!!");
        }
        
        // Instantiate and setup the overlap GameObject
        GameObject ob = Instantiate(pfOverlap) as GameObject;
        ob.transform.SetParent(this.cOverlap.transform);
        ob.transform.localScale = Vector3.one;
        ob.transform.localPosition = Vector3.zero;
#if UNITY_EDITOR
        ob.name = "OVERLAP_" + nameOverlap;
#endif
        BaseOverlap overlapScr = ob.GetComponent<BaseOverlap>();
        overlapScr.Init();
        return overlapScr;
    }

    /// <summary>
    /// Hides all currently visible overlaps.
    /// </summary>
    public void HideAllOverlaps()
    {
        BaseOverlap overlapScr = null;

        foreach (KeyValuePair<string, BaseOverlap> item in overlaps)
        {
            overlapScr = item.Value;
            // Skip if overlap is null or already hidden
            if (overlapScr == null || overlapScr.IsHide)
                continue;
            overlapScr.Hide();

            if (overlaps.Count <= 0)
                break;
        }
    }

    /// <summary>
    /// Gets an existing overlap instance if it has been created before.
    /// </summary>
    /// <typeparam name="T">The type of overlap to retrieve.</typeparam>
    /// <returns>The existing overlap instance, or null if not found.</returns>
    public T GetExistOverlap<T>() where T : BaseOverlap
    {
        string overlapName = typeof(T).Name;
        if (overlaps.ContainsKey(overlapName))
        {
            return overlaps[overlapName] as T;
        }
        return null;
    }

    /// <summary>
    /// Shows an overlap, creating it if it doesn't exist. Reuses existing instance if available.
    /// </summary>
    /// <typeparam name="T">The type of overlap to show.</typeparam>
    /// <param name="data">Optional data to pass to the overlap's Show method.</param>
    /// <param name="forceShowData">If true, forces the overlap to show even if already visible.</param>
    public void ShowOverlap<T>(object data = null, bool forceShowData = false) where T : BaseOverlap
    {
        string overlapName = typeof(T).Name;
        BaseOverlap result = null;

        if (curOverlap != null)
        {
            var curName = curOverlap.GetType().Name;
            if (curName.Equals(overlapName))
            {
                result = curOverlap;
            }
        }

        if (result == null)
        {
            if (!overlaps.ContainsKey(overlapName))
            {
                BaseOverlap overlapScr = GetNewOverLap<T>();
                if (overlapScr != null)
                {
                    overlaps.Add(overlapName, overlapScr);
                }
            }

            if (overlaps.ContainsKey(overlapName))
            {
                result = overlaps[overlapName];
            }
        }

        bool isShow = false;
        if (result != null)
        {
            if (forceShowData)
            {
                isShow = true;
            }
            else
            {
                if (result.IsHide)
                {
                    isShow = true;
                }
            }
        }

        if (isShow)
        {
            curOverlap = result;
            result.transform.SetAsLastSibling();
            result.Show(data);
        }
    }

    #endregion

    /// <summary>
    /// Loads a UI prefab from the Resources folder based on UI type and name.
    /// </summary>
    /// <param name="t">The type of UI element (Screen, Popup, Notify, or Overlap).</param>
    /// <param name="uiName">The name of the UI prefab to load (should match the class name).</param>
    /// <returns>The loaded GameObject prefab, or null if not found.</returns>
    private GameObject GetUIPrefab(UIType t, string uiName)
    {
        GameObject result = null;
        var defaultPath = "";
        if (result == null)
        {
            // Determine the resource path based on UI type
            switch (t)
            {
                case UIType.Screen:
                    {
                        defaultPath = SCREEN_RESOURCES_PATH + uiName;
                    }
                    break;
                case UIType.Popup:
                    {
                        defaultPath = POPUP_RESOURCES_PATH + uiName;
                    }
                    break;
                case UIType.Notify:
                    {
                        defaultPath = NOTIFY_RESOURCES_PATH + uiName;
                    }
                    break;
                case UIType.Overlap:
                    {
                        defaultPath = OVERLAP_RESOURCES_PATH + uiName;
                    }
                    break;
            }

            // Load the prefab from Resources folder
            result = Resources.Load(defaultPath) as GameObject;
        }
        return result;
    }
}
