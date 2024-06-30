using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class GirlControllerEnd : MonoBehaviour
{
    public Transform GirlRunTarget;
    public Transform LookPoint;
    public float TalkingDelay = 8f;
    private NavMeshAgent _agent;
    private Animator _animator;
    private bool _hasReachedDestination = false;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_agent != null && _animator != null)
        {
            if (_agent.remainingDistance > _agent.stoppingDistance)
            {
                _animator.SetBool("Idle", false);
                _animator.SetBool("Run", true);
                _hasReachedDestination = false;
            }
            else
            {
                _animator.SetBool("Run", false);
                _animator.SetBool("Idle", true);
                RotateToFaceLookPoint();

                if (!_hasReachedDestination)
                {
                    _hasReachedDestination = true;
                    StartCoroutine(StartTalkingAfterDelay(TalkingDelay));
                }
            }
        }
    }

    public void MoveToTarget()
    {
        if (_agent != null && GirlRunTarget != null)
        {
            _agent.SetDestination(GirlRunTarget.position);
        }
    }

    private void RotateToFaceLookPoint()
    {
        if (LookPoint != null)
        {
            Vector3 direction = (LookPoint.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    private IEnumerator StartTalkingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _animator.SetBool("Idle", false);
        _animator.SetBool("Talking", true);
    }

    public void EndTalking()
    {
        _animator.SetBool("Idle", true);
        _animator.SetBool("Talking", false);
    }
}