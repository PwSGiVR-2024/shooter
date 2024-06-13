using UnityEngine;
using UnityEngine.AI;

public class GirlController : MonoBehaviour
{
    public Transform caveEntrance; // Punkt, do kt�rego posta� ma biec
    private NavMeshAgent agent;
    private Animator animator;
    private bool shouldMove = false;
    private bool isDisappearing = false;
    private float disappearTime = 1f; // Czas do znikni�cia po osi�gni�ciu celu
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
            // Sprawdzenie, czy posta� osi�gn�a cel
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                // Je�eli osi�gn�a cel, zmie� stan animacji na idle
                animator.SetBool("isRunning", false);
                // Rozpocznij odliczanie do znikni�cia
                isDisappearing = true;
                disappearTimer = disappearTime;
            }
            else
            {
                // Je�eli posta� biegnie, ustaw animacj� biegu
                animator.SetBool("isRunning", true);
            }
        }

        if (isDisappearing)
        {
            // Odliczanie do znikni�cia postaci
            disappearTimer -= Time.deltaTime;
            if (disappearTimer <= 0f)
            {
                gameObject.SetActive(false); // Wy��czenie obiektu
            }
        }
    }

    public void StartMoving()
    {
        shouldMove = true;
        agent.SetDestination(caveEntrance.position);
    }
}