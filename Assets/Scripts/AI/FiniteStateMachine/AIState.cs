/// <summary>
/// Interface defining the contract for all AI states in the finite state machine.
/// Each state must implement these methods to handle state lifecycle and behavior.
/// </summary>
public interface AIState
{
    /// <summary>
    /// Returns the unique identifier for this state.
    /// </summary>
    /// <returns>The AIStateID enum value representing this state.</returns>
    AIStateID GetID();
    
    /// <summary>
    /// Called when entering this state. Use this to initialize state-specific behavior.
    /// </summary>
    /// <param name="agent">The AI agent that is entering this state.</param>
    void Enter(AIAgent agent);
    
    /// <summary>
    /// Called when exiting this state. Use this to clean up state-specific behavior.
    /// </summary>
    /// <param name="agent">The AI agent that is exiting this state.</param>
    void Exit(AIAgent agent);
    
    /// <summary>
    /// Called every frame while in this state. Contains the main state logic and transition checks.
    /// </summary>
    /// <param name="agent">The AI agent currently in this state.</param>
    void Update(AIAgent agent);
}
