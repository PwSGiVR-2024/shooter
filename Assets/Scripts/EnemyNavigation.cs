using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    [SerializeField] private float _enemySpeed = 2.5f;
    [SerializeField] private float _detectionRange = 30.0f;
    [SerializeField] private int _attackDamage = 10;
    [SerializeField] private float _attackRange = 1.25f;
    [SerializeField] private float _attackCooldown = 1.5f;

    private Transform _playerTransform;
    private PlayerHealth _playerHealth;
    private Animator _anim;
    private NavMeshAgent _agent;
    private Vector3 _lastKnownPlayerPosition;
    private bool _isDead = false;
    private bool _canAttack = true;

    private void Start()
    {
        _anim = GetComponent<Animator>();

        _playerTransform = GameObject.FindWithTag("Player").transform;
        _playerHealth = _playerTransform.GetComponentInChildren<PlayerHealth>();

        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _enemySpeed;

        EnemyHealth enemyHealth = gameObject.GetComponent<EnemyHealth>();
        enemyHealth.OnEnemyDeath += Die;
    }

    private void Update()
    {
        if (_isDead)
        {
            return;
        }
        DebugInput();
        TryAttackPlayer();
    }

    private void DebugInput()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Die();
            return;
        }
    }

    private void Die()
    {
        _isDead = true;
        _agent.enabled = false;
        _anim.SetTrigger("Die");
        Collider collider = GetComponent<Collider>();
        collider.enabled = false;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        GameObject spawner = new(name + "DropSpawner");
        spawner.transform.position = transform.position;
        spawner.AddComponent<GhoulDropSpawner>();
        spawner.GetComponent<GhoulDropSpawner>().Spawn(2.5f);

        Destroy(gameObject, 2f);
    }

    private void TryAttackPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, _playerTransform.position);
        if (distanceToPlayer <= _detectionRange)
        {
            _lastKnownPlayerPosition = _playerTransform.position;
            _agent.destination = _lastKnownPlayerPosition;
        }
        else
        {
            _agent.destination = _lastKnownPlayerPosition;
        }
        if (_agent.velocity.magnitude > 0.1f)
        {
            _anim.SetBool("Walking", true);
        }
        else
        {
            _anim.SetBool("Walking", false);
        }
        if (distanceToPlayer <= _attackRange && _canAttack)
        {
            StartCoroutine(AttackWithCooldown());
        }
    }

    private IEnumerator AttackWithCooldown()
    {
        _canAttack = false;
        AttackPlayer();
        yield return new WaitForSeconds(_attackCooldown);
        _canAttack = true;
    }

    private void AttackPlayer()
    {
        _playerHealth.TakeDamage(_attackDamage);
        _anim.SetTrigger("Attack");
    }
}