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
            // Sprawdzenie, czy posta� osi�gn�a cel
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                // Je�eli osi�gn�a cel, zmie� stan animacji na idle
                animator.SetBool("isRunning", false);
                // Wy��cz obiekt natychmiast
                gameObject.SetActive(false);
            }
            else
            {
                // Je�eli posta� biegnie, ustaw animacj� biegu
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