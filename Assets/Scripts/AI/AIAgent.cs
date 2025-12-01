using UnityEngine;
using UnityEngine.AI;

public class AIAgent : MonoBehaviour
{
    public AIStateID initState;
    public AIStateMachine stateMachine;
    public NavMeshAgent navMeshAgent;
    public Animator animator;
    public Transform target;

    void Awake()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stateMachine = new AIStateMachine(this);
        //Register states
        stateMachine.RegisterState(new AIIdleState());
        stateMachine.RegisterState(new AIChasePlayerState());
        stateMachine.ChangeState(initState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }
}
