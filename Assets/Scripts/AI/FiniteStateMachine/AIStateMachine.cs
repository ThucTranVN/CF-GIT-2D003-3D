using System;
using UnityEngine;

/// <summary>
/// Finite State Machine implementation for managing AI behavior states.
/// Handles state registration, transitions, and updates for AI agents.
/// </summary>
public class AIStateMachine
{
  /// <summary>
  /// Array storing all registered states, indexed by their AIStateID enum value.
  /// </summary>
  public AIState[] states;
  
  /// <summary>
  /// Reference to the AI agent that owns this state machine.
  /// </summary>
  public AIAgent agent;
  
  /// <summary>
  /// The ID of the currently active state.
  /// </summary>
  public AIStateID currentStateID;

  /// <summary>
  /// Initializes the state machine with the given agent and allocates state array.
  /// </summary>
  /// <param name="agent">The AI agent that will use this state machine.</param>
  public AIStateMachine(AIAgent agent)
  {
    this.agent = agent;
    // Calculate the number of states based on the AIStateID enum
    int numberOfStates = Enum.GetValues(typeof(AIStateID)).Length;
    states = new AIState[numberOfStates];
  }

  /// <summary>
  /// Registers a state with the state machine. States are stored by their ID.
  /// </summary>
  /// <param name="state">The state instance to register.</param>
  public void RegisterState(AIState state)
  {
    int index = (int)state.GetID();
    states[index] = state;
  }

  /// <summary>
  /// Retrieves a state instance by its ID.
  /// </summary>
  /// <param name="stateID">The ID of the state to retrieve.</param>
  /// <returns>The state instance, or null if not found.</returns>
  public AIState GetState(AIStateID stateID)
  {
    int index = (int)stateID;
    return states[index];
  }

  /// <summary>
  /// Updates the current state. Called every frame by the AI agent.
  /// </summary>
  public void Update()
  {
    GetState(currentStateID).Update(agent);
    Debug.Log($"Current state: {currentStateID}");
  }

  /// <summary>
  /// Transitions from the current state to a new state.
  /// Properly calls Exit() on the old state and Enter() on the new state.
  /// </summary>
  /// <param name="newStateID">The ID of the state to transition to.</param>
  public void ChangeState(AIStateID newStateID)
  {
    // Exit the current state
    GetState(currentStateID).Exit(agent);
    
    // Update the current state ID
    currentStateID = newStateID;
    
    // Enter the new state
    GetState(currentStateID).Enter(agent);
  }
}
