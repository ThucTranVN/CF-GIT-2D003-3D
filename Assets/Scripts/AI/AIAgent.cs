using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// AI Agent component that manages AI behavior using a finite state machine.
/// Handles navigation, animation, and state transitions for AI entities.
/// </summary>
public class AIAgent : MonoBehaviour
{
    [Header("State Machine Configuration")]
    /// <summary>
    /// The initial state the AI should start in when the game begins.
    /// </summary>
    public AIStateID initState;
    
    /// <summary>
    /// Reference to the state machine that manages all AI states and transitions.
    /// </summary>
    public AIStateMachine stateMachine;
    
    [Header("Component References")]
    /// <summary>
    /// NavMeshAgent component used for pathfinding and navigation.
    /// </summary>
    public NavMeshAgent navMeshAgent;
    
    /// <summary>
    /// Animator component used for controlling AI animations.
    /// </summary>
    public Animator animator;
    
    [Header("Target Configuration")]
    /// <summary>
    /// The target transform that the AI should track or chase (e.g., player position).
    /// </summary>
    public Transform target;

    /// <summary>
    /// Initializes component references before Start() is called.
    /// </summary>
    void Awake()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    /// <summary>
    /// Initializes the state machine and registers all available AI states.
    /// Sets the AI to start in the specified initial state.
    /// </summary>
    void Start()
    {
        // Create and initialize the state machine
        stateMachine = new AIStateMachine(this);
        
        // Register all available states with the state machine
        stateMachine.RegisterState(new AIIdleState());
        stateMachine.RegisterState(new AIChasePlayerState());
        
        // Transition to the initial state
        stateMachine.ChangeState(initState);
    }

    /// <summary>
    /// Updates the state machine every frame to handle state logic and transitions.
    /// </summary>
    void Update()
    {
        stateMachine.Update();
    }
}
