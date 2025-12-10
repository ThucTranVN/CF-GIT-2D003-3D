using System;
using System.Collections.Generic;

/// <summary>
/// Container for a group of listener actions that respond to a specific event type.
/// Manages the list of callbacks and handles broadcasting events to all registered listeners.
/// </summary>
public class ListenerGroup
{
    /// <summary>
    /// List of action callbacks that will be invoked when an event is broadcast.
    /// </summary>
    List<Action<object>> actions = new();

    /// <summary>
    /// Broadcasts an event value to all registered listener actions.
    /// </summary>
    /// <param name="value">The data/value to pass to all listeners (can be null).</param>
    public void BroadCast(object value)
    {
        // Invoke all registered actions with the broadcast value
        for (int i = 0; i < actions.Count; i++)
        {
            actions[i](value);
        }
    }

    /// <summary>
    /// Attaches/registers a new listener action to this group.
    /// Prevents duplicate registrations of the same action.
    /// </summary>
    /// <param name="action">The action callback to register.</param>
    public void Attach(Action<object> action)
    {
        // Check if action is already registered to prevent duplicates
        for (int i = 0; i < actions.Count; i++)
        {
            if(actions[i] == action)
            {
                return; // Action already exists, don't add duplicate
            }
        }
        // Add the new action to the list
        actions.Add(action);
    }

    /// <summary>
    /// Detaches/unregisters a listener action from this group.
    /// </summary>
    /// <param name="action">The action callback to remove.</param>
    public void Detach(Action<object> action)
    {
        // Find and remove the specified action
        for (int i = 0; i < actions.Count; i++)
        {
            if(actions[i] == action)
            {
                actions.Remove(action);
                break; // Action found and removed, exit loop
            }
        }
    }
}
