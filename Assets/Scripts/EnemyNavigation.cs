using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    public Transform Player;
    private NavMeshAgent _agent;
    public float DetectionRange = 30.0f;
    private Vector3 _lastKnownPlayerPosition;
    public float EnemySpeed = 3.0f;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = EnemySpeed;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, Player.position) <= DetectionRange)
        {
            _lastKnownPlayerPosition = Player.position;
            _agent.destination = _lastKnownPlayerPosition;
        }
        else
        {
            _agent.destination = _lastKnownPlayerPosition;
        }
    }
}