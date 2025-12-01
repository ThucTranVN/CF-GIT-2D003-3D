using UnityEngine;

public class AIIdleState : AIState
{
  private Vector3 playerDirection;
  private float maxSightDistance = 10f;

    public AIStateID GetID()
    {
        return AIStateID.Idle;
    }

    public void Enter(AIAgent agent)
    {
        
    }
    
    public void Exit(AIAgent agent)
    {
        
    }

    public void Update(AIAgent agent)
    {
      playerDirection = agent.target.position - agent.transform.position;
      if (playerDirection.magnitude > maxSightDistance) return;

      Vector3 agentDirection = agent.transform.forward;
      agentDirection.Normalize();
    float dot = Vector3.Dot(agentDirection, playerDirection);
      Debug.Log($"Dot: {dot}");
      if (dot >= 0)
      {
        agent.stateMachine.ChangeState(AIStateID.ChasePlayer);
      }
    }
}
