using UnityEngine;
using UnityEngine.AI;

public class GirlControllerEnd : MonoBehaviour
{
    public Transform target;
    public Transform player;
    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (agent != null && animator != null)
        {
            if (agent.remainingDistance > agent.stoppingDistance)
            {
                animator.SetBool("Idle", false);
                animator.SetBool("Run", true);
            }
            else
            {
                animator.SetBool("Run", false);
                animator.SetBool("Idle", true);
                RotateToFacePlayer();
            }
        }
    }

    public void MoveToTarget()
    {
        if (agent != null && target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    private void RotateToFacePlayer()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }
}