using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DadControllerEnd : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    public Transform player;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (navMeshAgent.velocity.magnitude > 0.1f)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
            RotateToFacePlayer();
        }
    }

    public void MoveToPoint(Vector3 point)
    {
        if (navMeshAgent != null)
        {
            navMeshAgent.SetDestination(point);
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