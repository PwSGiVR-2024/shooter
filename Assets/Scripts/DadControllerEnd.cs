using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class DadControllerEnd : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    public Transform LookPoint;
    public float TalkingDuration = 5.5f;
    private bool _isRotating = false;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_navMeshAgent.velocity.magnitude > 0.1f)
        {
            _animator.SetBool("isRunning", true);
            _isRotating = false;
        }
        else
        {
            if (_animator.GetBool("isRunning"))
            {
                _animator.SetBool("isRunning", false);
                _isRotating = true;
                StartCoroutine(TalkForDuration(TalkingDuration));
            }

            if (_isRotating)
            {
                RotateToFaceLookPoint();
            }
        }
    }

    public void MoveToPoint(Vector3 point)
    {
        if (_navMeshAgent != null)
        {
            _navMeshAgent.SetDestination(point);
        }
    }

    private void RotateToFaceLookPoint()
    {
        if (LookPoint != null)
        {
            Vector3 direction = (LookPoint.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            float rotationSpeed = 5f * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed);
        }
    }

    private IEnumerator TalkForDuration(float duration)
    {
        _animator.SetBool("Talking", true);
        yield return new WaitForSeconds(duration);
        _animator.SetBool("Talking", false);
    }
}