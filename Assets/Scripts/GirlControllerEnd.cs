using UnityEngine;
using UnityEngine.AI;

public class GirlControllerEnd : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveToTarget()
    {
        if (agent != null && target != null)
        {
            agent.SetDestination(target.position);
        }
    }
}