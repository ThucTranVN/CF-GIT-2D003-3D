using UnityEngine;

/// <summary>
/// AI state that handles idle behavior and player detection.
/// The AI remains idle until it detects the player within its sight range and field of view.
/// </summary>
public class AIIdleState : AIState
{
  /// <summary>
  /// Direction vector from the AI to the player target.
  /// </summary>
  private Vector3 playerDirection;
  
  /// <summary>
  /// Maximum distance at which the AI can detect the player (in units).
  /// </summary>
  private float maxSightDistance = 10f;

    /// <summary>
    /// Returns the unique identifier for the Idle state.
    /// </summary>
    /// <returns>AIStateID.Idle</returns>
    public AIStateID GetID()
    {
        return AIStateID.Idle;
    }

    /// <summary>
    /// Called when entering the idle state.
    /// Initialize idle-specific behavior here (e.g., play idle animation, stop movement).
    /// </summary>
    /// <param name="agent">The AI agent entering this state.</param>
    public void Enter(AIAgent agent)
    {
        // TODO: Implement idle initialization logic
        // Example: Play idle animation
        // Example: Stop NavMeshAgent movement
    }
    
    /// <summary>
    /// Called when exiting the idle state.
    /// Clean up idle-specific behavior here.
    /// </summary>
    /// <param name="agent">The AI agent exiting this state.</param>
    public void Exit(AIAgent agent)
    {
        // TODO: Implement idle cleanup logic if needed
    }

    /// <summary>
    /// Called every frame while in the idle state.
    /// Checks if the player is within sight range and in front of the AI.
    /// Transitions to ChasePlayer state if conditions are met.
    /// </summary>
    /// <param name="agent">The AI agent currently in this state.</param>
    public void Update(AIAgent agent)
    {
      // Calculate direction from AI to player
      playerDirection = agent.target.position - agent.transform.position;
      
      // Check if player is within sight distance
      if (playerDirection.magnitude > maxSightDistance) return;

      // Get the forward direction of the AI
      Vector3 agentDirection = agent.transform.forward;
      agentDirection.Normalize();
      
      // Normalize player direction for dot product calculation
      playerDirection.Normalize();
      
      // Calculate dot product to check if player is in front of AI
      // Dot product >= 0 means player is in front (or to the side), < 0 means behind
      float dot = Vector3.Dot(agentDirection, playerDirection);
      Debug.Log($"Dot: {dot}");
      
      // If player is in front of AI, transition to chase state
      if (dot >= 0)
      {
        agent.stateMachine.ChangeState(AIStateID.ChasePlayer);
      }
    }
}
