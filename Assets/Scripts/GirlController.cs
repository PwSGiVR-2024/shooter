using UnityEngine;
using UnityEngine.AI;

public class GirlController : MonoBehaviour
{
    public Transform caveEntrance;
    private NavMeshAgent agent;
    private Animator animator;
    private bool shouldMove = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (shouldMove)
        {
            // Sprawdzenie, czy postaæ osi¹gnê³a cel
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                // Je¿eli osi¹gnê³a cel, zmieñ stan animacji na idle
                animator.SetBool("isRunning", false);
                // Wy³¹cz obiekt natychmiast
                gameObject.SetActive(false);
            }
            else
            {
                // Je¿eli postaæ biegnie, ustaw animacjê biegu
                animator.SetBool("isRunning", true);
            }
        }
    }

    public void StartMoving()
    {
        shouldMove = true;
        agent.SetDestination(caveEntrance.position);
    }
}