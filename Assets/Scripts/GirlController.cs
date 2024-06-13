using UnityEngine;
using UnityEngine.AI;

public class GirlController : MonoBehaviour
{
    public Transform caveEntrance; // Punkt, do którego postaæ ma biec
    private NavMeshAgent agent;
    private Animator animator;
    private bool shouldMove = false;
    private bool isDisappearing = false;
    private float disappearTime = 1f; // Czas do znikniêcia po osi¹gniêciu celu
    private float disappearTimer = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (shouldMove && !isDisappearing)
        {
            // Sprawdzenie, czy postaæ osi¹gnê³a cel
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                // Je¿eli osi¹gnê³a cel, zmieñ stan animacji na idle
                animator.SetBool("isRunning", false);
                // Rozpocznij odliczanie do znikniêcia
                isDisappearing = true;
                disappearTimer = disappearTime;
            }
            else
            {
                // Je¿eli postaæ biegnie, ustaw animacjê biegu
                animator.SetBool("isRunning", true);
            }
        }

        if (isDisappearing)
        {
            // Odliczanie do znikniêcia postaci
            disappearTimer -= Time.deltaTime;
            if (disappearTimer <= 0f)
            {
                gameObject.SetActive(false); // Wy³¹czenie obiektu
            }
        }
    }

    public void StartMoving()
    {
        shouldMove = true;
        agent.SetDestination(caveEntrance.position);
    }
}