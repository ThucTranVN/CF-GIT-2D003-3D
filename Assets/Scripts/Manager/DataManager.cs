using UnityEngine;

/// <summary>
/// Manager responsible for accessing and modifying global game configuration data.
/// Provides centralized access to GlobalConfig ScriptableObject values.
/// </summary>
public class DataManager : BaseManager<DataManager>
{
    [SerializeField]
    /// <summary>
    /// Reference to the GlobalConfig ScriptableObject containing all game configuration values.
    /// </summary>
    private GlobalConfig GlobalConfig;

    /// <summary>
    /// Gets the current player movement speed from the global configuration.
    /// </summary>
    /// <returns>The movement speed value.</returns>
    public float GetMoveSpeed()
    {
        return GlobalConfig.MoveSpeed;
    }

    /// <summary>
    /// Sets the player movement speed in the global configuration.
    /// This change persists for the duration of the game session.
    /// </summary>
    /// <param name="value">The new movement speed value to set.</param>
    public void SetMoveSpeed(float value)
    {
        GlobalConfig.MoveSpeed = value;
    }
}
