using UnityEngine;
using UnityEngine.AI;

public class EnemyFlyingAIDemo : MonoBehaviour
{
    public float MoveSpeed;
    public float FlyHeight;
    public Transform target;

    private NavMeshAgent agent;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.speed = MoveSpeed;
        agent.baseOffset = FlyHeight;
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = target.position;
    }
}
