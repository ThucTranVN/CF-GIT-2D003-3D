using System;
using System.Collections.Generic;

public class ListenerManager : BaseManager<ListenerManager>
{
    private Dictionary<ListenType, ListenerGroup> listeners = new();

    public void BroadCast(ListenType type, object value = null)
    {
        if(listeners.ContainsKey(type) && listeners[type] != null)
        {
            listeners[type].BroadCast(value);
        }
    }

    public void Register(ListenType type, Action<object> action)
    {
        if (!listeners.ContainsKey(type))
        {
            listeners.Add(type, new ListenerGroup());
        }

        if(listeners[type] != null)
        {
            listeners[type].Attach(action);
        }
    }

    public void UnRegister(ListenType type, Action<object> action)
    {
        if(listeners.ContainsKey(type) && listeners[type] != null)
        {
            listeners[type].Detach(action);
        }
    }

    public void UnregisterAll(Action<object> action)
    {
        foreach (ListenType key in listeners.Keys)
        {
            UnRegister(key, action);
        }
    }

}
