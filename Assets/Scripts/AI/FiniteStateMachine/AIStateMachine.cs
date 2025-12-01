using System;
using UnityEngine;

public class AIStateMachine
{
  public AIState[] states;
  public AIAgent agent;
  public AIStateID currentStateID;

  public AIStateMachine(AIAgent agent)
  {
    this.agent = agent;
    int numberOfStates = Enum.GetValues(typeof(AIStateID)).Length;
    states = new AIState[numberOfStates];
  }

  public void RegisterState(AIState state)
  {
    int index = (int)state.GetID();
    states[index] = state;
  }

  public AIState GetState(AIStateID stateID)
  {
    int index = (int)stateID;
    return states[index];
  }

  public void Update()
  {
    GetState(currentStateID).Update(agent);
    Debug.Log($"Current state: {currentStateID}");
  }

  public void ChangeState(AIStateID newStateID)
  {
    GetState(currentStateID).Exit(agent);
    currentStateID = newStateID;
    GetState(currentStateID).Enter(agent);
  }
}
