using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Demo script demonstrating AI following behavior with NavMeshAgent.
/// AI follows a target, attacks when in range, and returns to starting position when target is lost.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class AIFollowTargetDemo : MonoBehaviour
{
    [Header("Target Configuration")]
    /// <summary>
    /// The target transform that the AI should follow and attack.
    /// </summary>
    public Transform target;
    
    /// <summary>
    /// The distance at which the AI will stop moving and start attacking.
    /// </summary>
    public float attackRange;

    /// <summary>
    /// Reference to the NavMeshAgent component for pathfinding.
    /// </summary>
    private NavMeshAgent agent;
    
    /// <summary>
    /// Reference to the Animator component for controlling animations.
    /// </summary>
    private Animator animator;
    
    /// <summary>
    /// Current distance from AI to target.
    /// </summary>
    private float distance;
    
    /// <summary>
    /// The starting position where the AI will return when target is lost.
    /// </summary>
    private Vector3 startingPoint;
    
    /// <summary>
    /// Flag to track whether path calculation should occur.
    /// </summary>
    private bool pathCalculate = true;

    /// <summary>
    /// Initializes component references and stores the starting position.
    /// </summary>
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        startingPoint = transform.position;
    }

    /// <summary>
    /// Updates AI behavior: follows target, attacks when in range, or returns to start if path is lost.
    /// </summary>
    void Update()
    {
        // Calculate distance to target
        distance = Vector3.Distance(transform.position, target.position);

        // If within attack range, stop and attack
        if(distance < attackRange)
        {
            agent.isStopped = true;
            animator.SetBool("Attatck", true);
        }
        else
        {
            // Resume movement
            agent.isStopped = false;

            // If no path exists and we should calculate, return to starting point
            if(!agent.hasPath && pathCalculate)
            {
                agent.destination = startingPoint;
                pathCalculate = false;
            }
            else
            {
                // Follow the target
                animator.SetBool("Attatck", false);
                agent.destination = target.position;
                pathCalculate = true;
            }
        }
    }
}
