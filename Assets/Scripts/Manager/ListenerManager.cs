using System;
using System.Collections.Generic;

/// <summary>
/// Manager for the observer pattern event system.
/// Handles registration, broadcasting, and cleanup of event listeners.
/// Allows decoupled communication between game systems.
/// </summary>
public class ListenerManager : BaseManager<ListenerManager>
{
    /// <summary>
    /// Dictionary mapping event types to their respective listener groups.
    /// Each ListenType has a ListenerGroup containing all subscribed actions.
    /// </summary>
    private Dictionary<ListenType, ListenerGroup> listeners = new();

    /// <summary>
    /// Broadcasts an event to all registered listeners of the specified type.
    /// </summary>
    /// <param name="type">The type of event to broadcast.</param>
    /// <param name="value">Optional data to pass to listeners (can be null).</param>
    public void BroadCast(ListenType type, object value = null)
    {
        // Check if any listeners exist for this event type
        if(listeners.ContainsKey(type) && listeners[type] != null)
        {
            listeners[type].BroadCast(value);
        }
    }

    /// <summary>
    /// Registers a listener action for a specific event type.
    /// The action will be called whenever the event is broadcast.
    /// </summary>
    /// <param name="type">The event type to listen for.</param>
    /// <param name="action">The action/callback to execute when the event is broadcast.</param>
    public void Register(ListenType type, Action<object> action)
    {
        // Create a new listener group if one doesn't exist for this event type
        if (!listeners.ContainsKey(type))
        {
            listeners.Add(type, new ListenerGroup());
        }

        // Attach the action to the listener group
        if(listeners[type] != null)
        {
            listeners[type].Attach(action);
        }
    }

    /// <summary>
    /// Unregisters a listener action from a specific event type.
    /// The action will no longer receive broadcasts for this event.
    /// </summary>
    /// <param name="type">The event type to stop listening for.</param>
    /// <param name="action">The action to remove from listeners.</param>
    public void UnRegister(ListenType type, Action<object> action)
    {
        // Remove the action from the listener group if it exists
        if(listeners.ContainsKey(type) && listeners[type] != null)
        {
            listeners[type].Detach(action);
        }
    }

    /// <summary>
    /// Unregisters a specific action from all event types.
    /// Useful for cleaning up when an object is destroyed.
    /// </summary>
    /// <param name="action">The action to remove from all event types.</param>
    public void UnregisterAll(Action<object> action)
    {
        // Iterate through all event types and unregister the action
        foreach (ListenType key in listeners.Keys)
        {
            UnRegister(key, action);
        }
    }
}
