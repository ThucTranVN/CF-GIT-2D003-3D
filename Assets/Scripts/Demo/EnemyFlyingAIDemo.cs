using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Demo script demonstrating flying AI behavior using NavMeshAgent.
/// Configures the NavMeshAgent to fly at a specific height and follow a target.
/// </summary>
public class EnemyFlyingAIDemo : MonoBehaviour
{
    [Header("Movement Configuration")]
    /// <summary>
    /// The movement speed of the flying AI.
    /// </summary>
    public float MoveSpeed;
    
    /// <summary>
    /// The height at which the AI should fly above the NavMesh.
    /// </summary>
    public float FlyHeight;
    
    [Header("Target Configuration")]
    /// <summary>
    /// The target transform that the flying AI should follow.
    /// </summary>
    public Transform target;

    /// <summary>
    /// Reference to the NavMeshAgent component for pathfinding.
    /// </summary>
    private NavMeshAgent agent;
    
    /// <summary>
    /// Reference to the Animator component for controlling animations.
    /// </summary>
    private Animator animator;

    /// <summary>
    /// Initializes component references and configures NavMeshAgent for flying behavior.
    /// </summary>
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
        // Configure NavMeshAgent for flying
        agent.speed = MoveSpeed;
        agent.baseOffset = FlyHeight; // Sets the height offset above the NavMesh
    }

    /// <summary>
    /// Updates the NavMeshAgent destination to follow the target every frame.
    /// </summary>
    void Update()
    {
        agent.destination = target.position;
    }
}
