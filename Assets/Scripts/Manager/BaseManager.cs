using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for singleton managers in the game.
/// Provides singleton pattern implementation with automatic instance management.
/// Managers using this base class persist across scene loads and ensure only one instance exists.
/// </summary>
/// <typeparam name="T">The derived manager type (e.g., GameManager, UIManager).</typeparam>
public class BaseManager<T> : MonoBehaviour where T : BaseManager<T>
{
    /// <summary>
    /// Static reference to the singleton instance of this manager.
    /// </summary>
    private static T instance;

    /// <summary>
    /// Public accessor for the singleton instance.
    /// Automatically finds an existing instance if one hasn't been set yet.
    /// </summary>
    public static T Instance
    {
        get
        {
            // Lazy initialization: find instance if not yet set
            if (instance == null)
            {
                instance = Object.FindFirstObjectByType<T>();

                // Log error if no instance exists in the scene
                if (instance == null)
                {
                    Debug.LogError($"No {typeof(T).Name} Singleton Instance.");
                }
            }

            return instance;
        }
    }

    /// <summary>
    /// Checks if a singleton instance currently exists.
    /// Useful for safe access without creating errors.
    /// </summary>
    public static bool HasInstance
    {
        get
        {
            return (instance != null);
        }
    }

    /// <summary>
    /// Called when the GameObject is initialized.
    /// Ensures only one instance of this manager exists.
    /// </summary>
    protected virtual void Awake()
    {
        CheckInstance();
    }

    /// <summary>
    /// Validates and sets the singleton instance.
    /// Destroys duplicate instances and marks the first one to persist across scenes.
    /// </summary>
    /// <returns>True if this is the valid singleton instance, false if it was destroyed.</returns>
    protected bool CheckInstance()
    {
        // If no instance exists yet, this becomes the singleton
        if (instance == null)
        {
            instance = (T)this;
            // Persist across scene loads
            DontDestroyOnLoad(this);
            return true;
        }
        // If this is already the instance, it's valid
        else if (instance == this)
        {
            return true;
        }

        // If another instance already exists, destroy this duplicate
        Object.Destroy(this.gameObject);
        return false;
    }
}
