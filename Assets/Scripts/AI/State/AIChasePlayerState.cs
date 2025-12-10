using UnityEngine;

/// <summary>
/// AI state that handles chasing the player target.
/// This state is activated when the AI detects the player within its sight range.
/// </summary>
public class AIChasePlayerState : AIState
{
    /// <summary>
    /// Returns the unique identifier for the ChasePlayer state.
    /// </summary>
    /// <returns>AIStateID.ChasePlayer</returns>
    public AIStateID GetID()
    {
        return AIStateID.ChasePlayer;
    }

    /// <summary>
    /// Called when entering the chase state.
    /// Initialize chase-specific behavior here (e.g., set navigation target, play chase animation).
    /// </summary>
    /// <param name="agent">The AI agent entering this state.</param>
    public void Enter(AIAgent agent)
    {
        // TODO: Implement chase initialization logic
        // Example: Set NavMeshAgent destination to target position
        // Example: Play chase animation
    }

    /// <summary>
    /// Called when exiting the chase state.
    /// Clean up chase-specific behavior here (e.g., stop navigation, reset animations).
    /// </summary>
    /// <param name="agent">The AI agent exiting this state.</param>
    public void Exit(AIAgent agent)
    {
        // TODO: Implement chase cleanup logic
        // Example: Stop NavMeshAgent movement
    }

    /// <summary>
    /// Called every frame while in the chase state.
    /// Contains the main chase logic (e.g., update navigation target, check for state transitions).
    /// </summary>
    /// <param name="agent">The AI agent currently in this state.</param>
    public void Update(AIAgent agent)
    {
        // TODO: Implement chase behavior logic
        // Example: Update NavMeshAgent destination to current target position
        // Example: Check if target is out of range and transition back to Idle state
    }
    
}
