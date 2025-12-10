using UnityEngine;

/// <summary>
/// ScriptableObject containing global game configuration values.
/// This asset stores all configurable parameters for players, AI, and UI systems.
/// Create instances via: Assets > Create > Scriptable Objects > GlobalConfig
/// </summary>
[CreateAssetMenu(fileName = "GlobalConfig", menuName = "Scriptable Objects/GlobalConfig")]
public class GlobalConfig : ScriptableObject
{
    [Header("Player")]
    /// <summary>
    /// The movement speed of the player character (units per second).
    /// </summary>
    public float MoveSpeed;
    
    /// <summary>
    /// The rotation/turning speed of the player character (degrees per second).
    /// </summary>
    public float TurnSpeed;
    
    /// <summary>
    /// The height the player can jump (in units).
    /// </summary>
    public float JumpHeight;
    
    /// <summary>
    /// The maximum health value for the player.
    /// </summary>
    public float MaxHealth;

    [Header("AI")]
    /// <summary>
    /// The maximum health value for AI entities.
    /// </summary>
    public float AIMaaxHeath;
    
    /// <summary>
    /// The force applied to AI entities when they die (for ragdoll physics).
    /// </summary>
    public float DieForce = 10f;
    
    /// <summary>
    /// The minimum distance the AI will maintain from its target before stopping.
    /// </summary>
    public float StopDistanceLimit = 1.5f;

    [Header("UI")]
    /// <summary>
    /// The duration of the game loading screen in seconds.
    /// </summary>
    public float GameLoadingTime = 2f;
}
