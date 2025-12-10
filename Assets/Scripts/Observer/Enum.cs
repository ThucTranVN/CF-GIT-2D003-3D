/// <summary>
/// Enumeration of all event types that can be broadcast through the ListenerManager.
/// Used for the observer pattern to enable decoupled communication between systems.
/// </summary>
public enum ListenType
{
    /// <summary>
    /// Generic catch-all event type (not commonly used).
    /// </summary>
    ANY = 0,
    
    /// <summary>
    /// Event broadcast when the player dies.
    /// </summary>
    ON_PLAYER_DEATH,
    
    /// <summary>
    /// Event broadcast when user information is updated.
    /// </summary>
    ON_UPDATE_USER_INFO,
    
    /// <summary>
    /// Event broadcast when the player's health value changes.
    /// </summary>
    ON_UPDATE_PLAYER_HEALTH,
    
    /// <summary>
    /// Event broadcast when the player switches weapons.
    /// </summary>
    ON_PLAYER_CHANGE_WEAPON,
    
    /// <summary>
    /// Event broadcast when the player's coin count changes.
    /// </summary>
    ON_PLAYER_UPDATE_COIN,
    
    /// <summary>
    /// Event broadcast when the game timer value changes.
    /// </summary>
    ON_TIMER_CHANGE
}

/// <summary>
/// Enumeration of UI element types used by the UIManager.
/// Determines which container and resource path to use for each UI element.
/// </summary>
public enum UIType
{
    /// <summary>
    /// Unknown or uninitialized UI type.
    /// </summary>
    Unknown = 0,
    
    /// <summary>
    /// Full-screen UI element (e.g., main menu, game screen).
    /// </summary>
    Screen = 1,
    
    /// <summary>
    /// Popup dialog UI element (e.g., settings, rewards).
    /// </summary>
    Popup = 2,
    
    /// <summary>
    /// Notification UI element (e.g., temporary messages, alerts).
    /// </summary>
    Notify = 3,
    
    /// <summary>
    /// Overlay UI element that covers the screen (e.g., fade effects, loading overlays).
    /// </summary>
    Overlap = 4
}

/// <summary>
/// Enumeration of AI state identifiers for the finite state machine.
/// Each value corresponds to a specific AI behavior state.
/// </summary>
public enum AIStateID
{
    /// <summary>
    /// Idle state - AI is stationary and looking for targets.
    /// </summary>
    Idle = 0,
    
    /// <summary>
    /// Chase state - AI is actively pursuing the player target.
    /// </summary>
    ChasePlayer = 1
}