using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIFollowTargetDemo : MonoBehaviour
{
    public Transform target;
    public float attackRange;

    private NavMeshAgent agent;
    private Animator animator;
    private float distance;
    private Vector3 startingPoint;
    private bool pathCalculate = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        startingPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, target.position);

        if(distance < attackRange)
        {
            agent.isStopped = true;
            animator.SetBool("Attatck", true);
        }
        else
        {
            agent.isStopped = false;

            if(!agent.hasPath && pathCalculate)
            {
                agent.destination = startingPoint;
                pathCalculate = false;
            }
            else
            {
                animator.SetBool("Attatck", false);
                agent.destination = target.position;
                pathCalculate = true;
            }
        }
    }
}
